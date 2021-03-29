using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using FotoShop.wwwroot.Classes;
using FotoShop.wwwroot.Classes.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Pages
{
    public class UploadImage : PageModel
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public UploadImage(IWebHostEnvironment hostEnvironment)
        {
            this._hostEnvironment = hostEnvironment;
        }
        
        public void OnGet()
        {
            
        }
        
        public async Task<IActionResult> OnPostUpload(string description, string categoryName, float price)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(rootPath + "/images/" + fileName + extension);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }
                Photo photo = new Photo();
                photo.Photo_path = $"/images/{fileName + extension}";
                photo.Price = price;
                photo.Description = description;
                photo.Category_name = categoryName;
                new PhotoRepository().Add(photo);
            }
            
            return Redirect("/UploadImage");
        }

        public async void OnPostDelete()
        {
            string rootPath = _hostEnvironment.WebRootPath;
            try
            {
                if (System.IO.File.Exists(Path.Combine(rootPath + "/images/gJEQ7vy.png")))
                {
                    System.IO.File.Delete(Path.Combine(rootPath + "/images/gJEQ7vy.png"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}