using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class PhotoPageModel : PageModel
    {
        public Photo PagePhoto { get; set; }

        public string Hidden { get; set; } = "hidden";

        public IActionResult OnGet(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return Redirect("Index");
            }

            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            PagePhoto = repo.Get(id);

            if (PagePhoto == null)
            {
                return Redirect("Shop");
            }

            string account = GetAccountType();
            if (account == "admin")
            {
                Hidden = "";
            }
            return Page();
        }

        public JsonResult OnPostSavePhoto([FromBody] Photo photo)
        {
            string account = GetAccountType();
            if (account == "admin")
            {
                Hidden = "";
                photo.Price = photo.Price.Replace(',', '.');
                if (Regex.IsMatch(photo.Price, @"[\d]{1,3}([.][\d]{1,2})?"))
                {
                    using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
                    if (repo.UpdatePhoto(photo))
                    {
                        return new JsonResult("success");
                    }
                }
                return new JsonResult("failed");
            }
            return new JsonResult("redirect");
        }

        public IActionResult OnPostDelete(string id)
        {
            string account = GetAccountType();
            if (account == "admin")
            {
                HardDriveUtils.DeleteImage(id);
                return Redirect("Shop");
            }
            return RedirectToPage("PhotoPage", new { id = id });
        }

        public string GetAccountType()
        {
            string accId = Request.Cookies["UserLoggedIn"];
            string accType = "user";
            if (!String.IsNullOrEmpty(accId))
            {
                using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
                accType = repo.GetFromAccount("Account_type", accId);
            }
            return accType;
        }

        public string GetPhotoPath()
        {
            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            string path = repo.GetFromPhoto("Photo_path", PagePhoto.Photo_id);
            return string.Format("/Images/ProductImages/{0}", path);
        }
    }
}
