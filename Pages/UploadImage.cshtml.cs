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
        [BindProperty, Required(ErrorMessage = "Gelieve een image toe te voegen!")]
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
                    string imagesDir = Path.Combine(new DirectoryInfo(
                        Directory.GetCurrentDirectory()).FullName, "wwwroot", "Images", "ProductImages");
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName); 
                    string file = fileName + extension;
                    string path = Path.Combine(imagesDir,NewPhoto.Category_name, file);
                    NewPhoto.Photo_path = string.Format("{0}/{1}{2}", NewPhoto.Category_name, fileName, extension);
                    using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
                    repo.Add(NewPhoto);
                    using (var fileStream = new FileStream(path, FileMode.Create))
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