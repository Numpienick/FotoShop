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
using Microsoft.AspNetCore.Http;
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

        [BindProperty] public string PhotoId {get;set;}
        public IActionResult OnPostSubmitWinkelwagen()
        {
            using OrderRepository repo = new OrderRepository(DbUtils.GetDbConnection());
            var Cookie1 = Request.Cookies["UserLoggedIn"];
            var OrderCookie = Request.Cookies["Order"];
            if (OrderCookie == null)
            {
                using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
                string downloadlink = "Randomlinkdit";
                var NewOrder = repoAdd.Add(Cookie1, downloadlink, PhotoId);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(9999999);  
                Response.Cookies.Append("Order", NewOrder.Placed_order_id);
                Response.Cookies.Append("ShoppingCard", PhotoId);
            }
            else
            {
                using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
                var Checkfoto = repoAdd.CheckFoto(OrderCookie, PhotoId);
                if (Checkfoto == null)
                {
                    using OrderRepository repoAddN = new OrderRepository(DbUtils.GetDbConnection());
                    repoAddN.InsertPhoto(OrderCookie, PhotoId);
                    Response.Cookies.Append("ShoppingCard", PhotoId);
                }
                using OrderRepository repoAdd2 = new OrderRepository(DbUtils.GetDbConnection());
                var Count = repoAdd2.GetPhoto(OrderCookie).Count().ToString();
                Response.Cookies.Append("ShoppingCartI", Count);
            }
            return Redirect($"PhotoPage?Id={PhotoId}");
        }
    }
}
