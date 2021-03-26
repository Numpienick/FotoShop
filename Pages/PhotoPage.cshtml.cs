using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class PhotoPageModel : PageModel
    {
        public string PhotoId { get; set; }

        public void OnGet(string id)
        {
            PhotoId = id;
        }

        public string GetPhotoPath()
        {
            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            string path = repo.GetPhotoPath(PhotoId);
            return string.Format("/Images/ProductImages/{0}", path);
        }
    }
}
