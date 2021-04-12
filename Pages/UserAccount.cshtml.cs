using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FotoShop.Pages
{
    public class UserAccountModel : PageModel
    {
        public User UserName { get; set; }
        public IActionResult OnGet()
        {
            using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
            UserName = repo.GetFromAccount("*", Request.Cookies["UserLoggedIn"]);
            if (UserName == null || UserName.Account_type != "user")
            {
                return Redirect("Index");
            }

            return Page();
        }

        public PartialViewResult OnGetUpdateUserInfoPartial()
        {
            using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
            User user = repo.GetFromAccount("Email, Password AS User_Password, First_name, Last_name",
                Request.Cookies["UserLoggedIn"]);
            UpdateUserInfo userInfo = new UpdateUserInfo
            {
                Email = user.Email,
                First_name = user.First_name,
                Last_name = user.Last_name
            };
            return new PartialViewResult
            {
                ViewName = "Partials/_UpdateUserInfoPartial",
                ViewData = new ViewDataDictionary<UpdateUserInfo>(ViewData, userInfo)
            };
        }

        public PartialViewResult OnGetOrdersPartial()
        {
            using OrderRepository repo = new OrderRepository(DbUtils.GetDbConnection());
            var orders = repo.GetOrdersFromAccount(Request.Cookies["UserLoggedIn"]);
            return new PartialViewResult
            {
                ViewName = "Partials/_OrdersPartial",
                ViewData = new ViewDataDictionary<List<Order>>(ViewData, orders)
            };
        }

        public PartialViewResult OnPostUpdateUserInfoPartial(UpdateUserInfo user)
        {
            if (ModelState.IsValid)
            {
                using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
                string oldPw = repo.GetFromAccount("Password AS User_Password", Request.Cookies["UserLoggedIn"]).User_Password;
                if (user.OldPassword == oldPw)
                {
                    var newPw = oldPw;
                    if (!String.IsNullOrEmpty(user.Password))
                    {
                        newPw = user.Password;
                    }
                    using UserRepository updateRepo = new UserRepository(DbUtils.GetDbConnection());
                    string updateString = String.Format(@"Email='{0}', 
                        Password='{1}', First_name='{2}', Last_name='{3}'",
                        user.Email, newPw, user.First_name, user.Last_name);
                    if (!updateRepo.Update(updateString, Request.Cookies["UserLoggedIn"]))
                    {
                        ViewData.ModelState.AddModelError("", "Er is iets fout gegaan");
                    }
                }
                else
                {
                    ViewData.ModelState.AddModelError("", "Uw huidige wachtwoord is incorrect ingevuld");
                }
            }
            return new PartialViewResult
            {
                ViewName = "Partials/_UpdateUserInfoPartial",
                ViewData = new ViewDataDictionary<UpdateUserInfo>(ViewData, user)
            };
        }
    }
}
