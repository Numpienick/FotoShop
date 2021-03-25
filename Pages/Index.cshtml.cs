using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace FotoShop.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public PartialViewResult OnGetLogInModalPartial()
        {
            return new PartialViewResult
            {
                ViewName = "Partials/_LogInModalPartial",
                ViewData = new ViewDataDictionary<LogIn>(ViewData, new LogIn { })
            };
        }

        public PartialViewResult OnPostLogInModalPartial(LogIn logIn)
        {
            if (ModelState.IsValid)
            {
                using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
                string id = repo.LogIn(logIn.Email, logIn.Password);
                if (String.IsNullOrEmpty(id))
                {
                    ViewData.ModelState.AddModelError("",
                        "Uw e-mail adres en wachtwoord combinatie is niet correct.");
                }
                else
                {
                    using UserRepository codeRepo = new UserRepository(DbUtils.GetDbConnection());
                    Response.Cookies.Append("UserLoggedIn", id);
                }
            }

            return new PartialViewResult
            {
                ViewName = "Partials/_LogInModalPartial",
                ViewData = new ViewDataDictionary<LogIn>(ViewData, new LogIn { })
            };
        }

        public PartialViewResult OnGetRegisterModalPartial()
        {
            return new PartialViewResult
            {
                ViewName = "Partials/_RegisterModalPartial",
                ViewData = new ViewDataDictionary<User>(ViewData, new User { })
            };
        }

        public PartialViewResult OnPostRegisterModalPartial(User user)
        {
            if (ModelState.IsValid)
            {
                using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
                if (!repo.Create(user))
                {
                    ViewData.ModelState.AddModelError("", "Er bestaat al een account met dit e-mail adres");
                }
            }

            return new PartialViewResult
            {
                ViewName = "Partials/_RegisterModalPartial",
                ViewData = new ViewDataDictionary<User>(ViewData, user)
            };
        }
    }
}