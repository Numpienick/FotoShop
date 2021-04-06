using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FotoShop.Classes;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class UploadImage : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Gelieve een foto toe te voegen!")]
        public IFormFile ImageFile { get; set; }
        
        [BindProperty]
        public Photo NewPhoto { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostUpload()
        {
            if (ModelState.IsValid)
            {
                NewPhoto.Price = NewPhoto.Price.Replace(',', '.');
                if (float.TryParse(NewPhoto.Price, out float price ))
                {
                    string file = HardDriveUtils.GetFilePath(ImageFile);
                    string dirPath = HardDriveUtils.GetDirectoryPath(ImageFile, NewPhoto.Category_name);
                    NewPhoto.Photo_path = string.Format("{0}/{1}", NewPhoto.Category_name, file);
                    using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
                    repo.Add(NewPhoto);
                    await using (var fileStream = new FileStream(dirPath, FileMode.Create))
                     {
                         await ImageFile.CopyToAsync(fileStream);
                     }
                    ModelState.Clear();
                    return Redirect("UploadImage"); 
                }
            }
            return Page();
        }
    }
}