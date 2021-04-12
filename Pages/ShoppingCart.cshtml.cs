using System;
using System.Collections.Generic;
using System.Net;
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
            if (photoList.Count == 0)
            {
                Response.Cookies.Append("EmptyShoppingCard", "Empty");
            }
            else
            {
                Response.Cookies.Delete("EmptyShoppingCard");
            }
            return photoList;
        }

        [BindProperty] public string ImgId { get; set; }
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
            Hidden = "";
        }

        public decimal TotalPrice()
        {
            decimal totPrice = 0;
            foreach (var photoid in GetAllPhoto())
            {
                using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
                decimal price = repo.GetPrice(photoid);
                TotPrice += price;

            }
            return totPrice;
        }
    }
}