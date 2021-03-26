using System.Collections.Generic;
using FotoShop.wwwroot.Classes;
using FotoShop.wwwroot.Classes.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class ModernUrban : PageModel
    {
        public void OnGet()
        {

        }

        public IEnumerable<Photo> AllPhotos
        {
            get
            {
                return new PhotoRepository().GetList(category: "ModernUrban");

            }
        }

    }
}