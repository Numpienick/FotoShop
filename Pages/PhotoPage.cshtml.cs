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

            if(PagePhoto == null)
            {
                return Redirect("Shop");
            }
            GetAccountType();
            return Page();
        }

        public JsonResult OnPostSavePhoto([FromBody] Photo photo)
        {
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

        public IActionResult OnPostDelete(string id)
        {
            var imagesDir = Path.Combine(new DirectoryInfo(
                Directory.GetCurrentDirectory()).FullName, "wwwroot", "Images", "ProductImages");

            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            repo.GetFromPhoto("Photo_path", id);

            using PhotoRepository delRepo = new PhotoRepository(DbUtils.GetDbConnection());
            delRepo.Delete(id);

            var imagePath = Path.Combine(imagesDir, "Photo_path");
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            return Redirect("Shop");
        }

        public string GetAccountType()
        {
            string accId = Request.Cookies["UserLoggedIn"];
            string accType = "user";
            if (!String.IsNullOrEmpty(accId))
            {
                using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
                accType = repo.GetFromAccount("Account_type", accId);
                if (!String.IsNullOrEmpty(accType))
                {
                    if (accType == "admin")
                    {
                        Hidden = "";
                    }
                }
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
