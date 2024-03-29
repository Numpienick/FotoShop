﻿using System;
using System.Collections.Generic;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class ShoppingCart : PageModel
    {
        public string Hidden { get; set; } = "Hidden";

        public IActionResult OnGet()
        {
            using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
            var user = repo.GetFromAccount("*", Request.Cookies["UserLoggedIn"]);
            if (user == null || string.IsNullOrEmpty(user.Account_type))
            {
                return Redirect("Index");
            }
            return Page();
        }

        public List<int> GetAllPhoto()
        {
            var OrderCookie = Request.Cookies["Order"];
            using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
            var AllPhoto = repoAdd.GetPhoto(OrderCookie);
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
                Response.Cookies.Delete("ShoppingCartI");
            }
            else
            {
                var items = photoList.Count;
                Response.Cookies.Delete("EmptyShoppingCard");
            }
            return photoList;
        }

        [BindProperty] public string ImgId { get; set; }

        public void OnPostDelete()
        {
            var OrderCookie = Request.Cookies["Order"];
            using OrderRepository repoAdd = new OrderRepository(DbUtils.GetDbConnection());
            repoAdd.DeletePhoto(ImgId, OrderCookie);
            using OrderRepository repoAdd2 = new OrderRepository(DbUtils.GetDbConnection());
            var MaxItems = repoAdd2.GetPhoto(OrderCookie).Count.ToString();
            Response.Cookies.Append("ShoppingCartI", MaxItems);
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