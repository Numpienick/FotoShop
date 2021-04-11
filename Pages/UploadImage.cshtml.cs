using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Imaging;
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
            if (!ModelState.IsValid) return Page();
            
            NewPhoto.Price = NewPhoto.Price.Replace(',', '.');

            if (!float.TryParse(NewPhoto.Price, out var price)) return Page();
            
            var file = HardDriveUtils.GetFilePath(ImageFile);
            var dirPath = HardDriveUtils.GetDirectoryPath(ImageFile, NewPhoto.Category_name);
                    
            NewPhoto.Photo_path = $"{NewPhoto.Category_name}/{file}";

            await using (var memoryStream = new MemoryStream())
            {
                await ImageFile.CopyToAsync(memoryStream);
                         
                // Add watermark
                var watermarkedStream = new MemoryStream();
                using (var img = Image.FromStream(memoryStream))
                {
                    using (var graphic = Graphics.FromImage(img))
                    {
                        var font = new Font(FontFamily.GenericSansSerif, (img.Width/10), FontStyle.Bold, GraphicsUnit.Pixel);
                        var color = Color.FromArgb(155, 255, 255, 255);
                        var brush = new SolidBrush(color);
                        var point = new Point(0, img.Height - (img.Height/2));

                        graphic.DrawString("  Hoekstra Fotografie", font, brush, point);
                        img.Save(watermarkedStream, ImageFormat.Jpeg);
                        
                        await using (var fileStream = new FileStream(dirPath, FileMode.Create))
                        {
                            watermarkedStream.WriteTo(fileStream);
                        }
                        
                    }
                }

            }
            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            repo.Add(NewPhoto);
                    
            ModelState.Clear();
            return Redirect("UploadImage");
        }
        
        //https://edi.wang/post/2018/10/12/add-watermark-to-uploaded-image-aspnet-core
    }
}