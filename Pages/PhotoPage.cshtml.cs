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

            if (PagePhoto == null)
            {
                return Redirect("Shop");
            }

            using UserRepository userRepo = new UserRepository(DbUtils.GetDbConnection());
            var acc = userRepo.GetFromAccount("Account_type", Request.Cookies["UserLoggedIn"]);
            if (acc != null && acc.Account_type == "admin")
            {
                Hidden = "";
            }
            return Page();
        }

        public JsonResult OnPostSavePhoto([FromBody] Photo photo)
        {
            using UserRepository userRepo = new UserRepository(DbUtils.GetDbConnection());
            var acc = userRepo.GetFromAccount("Account_type", Request.Cookies["UserLoggedIn"]);
            if (acc != null && acc.Account_type == "admin")
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
            using UserRepository userRepo = new UserRepository(DbUtils.GetDbConnection());
            var acc = userRepo.GetFromAccount("Account_type", Request.Cookies["UserLoggedIn"]);
            if (acc != null && acc.Account_type == "admin")
            {
                HardDriveUtils.DeleteImage(id);
                return Redirect("Shop");
            }
            return RedirectToPage("PhotoPage", new { id = id });
        }

        public string GetPhotoPath()
        {
            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            string path = repo.GetFromPhoto("Photo_path", PagePhoto.Photo_id);
            return string.Format("/Images/ProductImages/{0}", path);
        }

        [BindProperty] public string PhotoId { get; set; }
        public IActionResult OnPostSubmitWinkelwagen()
        {
            var userId = Request.Cookies["UserLoggedIn"];
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("PhotoPage", new { id = PhotoId });
            }
            using OrderRepository repo = new OrderRepository(DbUtils.GetDbConnection());
            var OrderCookie = Request.Cookies["Order"];
            if (OrderCookie == null)
            {
                using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
                string downloadlink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                var NewOrder = repoAdd.Add(userId, downloadlink, PhotoId);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(9999999);
                Response.Cookies.Append("Order", NewOrder.Placed_order_id);
                Response.Cookies.Append("ShoppingCartAdd", PhotoId);
            }
            else
            {
                using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
                var Checkfoto = repoAdd.CheckFoto(OrderCookie, PhotoId);
                if (Checkfoto == null)
                {
                    using OrderRepository repoAddN = new OrderRepository(DbUtils.GetDbConnection());
                    repoAddN.InsertPhoto(OrderCookie, PhotoId);
                    Response.Cookies.Append("ShoppingCartAdd", PhotoId);
                }

                using OrderRepository repoAdd2 = new OrderRepository(DbUtils.GetDbConnection());
                var Count = repoAdd2.GetPhoto(OrderCookie).Count().ToString();
                Response.Cookies.Append("ShoppingCartI", Count);
            }
            return Redirect($"PhotoPage?Id={PhotoId}");
        }
    }
}