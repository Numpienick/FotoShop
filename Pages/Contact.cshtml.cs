using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class Contact : PageModel
    {
        public void OnGet()
        {
            
        }

        [BindProperty]
        public DBContact GetDbContact { get; set; }
        
        public IActionResult OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                new ContactRepository().InsertNewContact(GetDbContact);

                ModelState.Clear();
            }
            
            
            return Page();
        }
    }
}