using System;
using System.Collections.Generic;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class ShoppingCart : PageModel
    {
        
        public void OnGet()
        {
            
        }

        public List<int> GetAllPhoto()
        {
            var OrderCookie = Request.Cookies["Order"];
            using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
            var AllPhoto = repoAdd.GetPhoto(Convert.ToInt32(OrderCookie));
            return AllPhoto;
        }

        public IList<Photo> GetPhoto()
        {
            IList<Photo> photoList = new List<Photo>();
            foreach (var photoId in GetAllPhoto())
            {
                using PhotoRepository repoAdd = new PhotoRepository(DbUtils.GetDbConnection());
                var photo = repoAdd.Get(photoId.ToString());
                photoList.Add(photo);
            }
            return photoList;
        }
        
        [BindProperty] public int ImgId { get; set; }
        public string Hidden { get; set; } = "hidden";
        public void OnPostDelete()
        {
            var OrderCookie = Request.Cookies["Order"];
            using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
            repoAdd.DeletePhoto(ImgId, OrderCookie);
        }

        public void OnPostOrderSucces()
        {
            var OrderCookie = Request.Cookies["Order"];
            using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
            repoAdd.OrderSucces(OrderCookie);
            Response.Cookies.Delete("Order");
            Response.Cookies.Delete("ShoppingCard");
            Hidden = "";
        }
        
        public decimal TotalPrice()
        {
            decimal TotPrice = 0;
            foreach (var photoid in GetAllPhoto())
            {
                using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
                decimal price = repo.GetPrice(photoid);
                TotPrice += price;

            }
            return TotPrice;
        }
    }
}