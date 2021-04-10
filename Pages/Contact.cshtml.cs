using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class Contact : PageModel
    {
        public string Hidden { get; set; } = "hidden";
        public void OnGet()
        {

        }

        [BindProperty]
        public DBContact GetDbContact { get; set; }

        public void OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                using ContactRepository repo = new ContactRepository(DbUtils.GetDbConnection());
                repo.InsertNewContact(GetDbContact);
                ModelState.Clear();
                Hidden = "";
                GetDbContact = new DBContact();
            }
        }
    }
}