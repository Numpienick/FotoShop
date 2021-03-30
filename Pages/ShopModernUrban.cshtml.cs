using System.Collections.Generic;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
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
                using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
                return repo.GetList("ModernUrban");
            }
        }

    }
}