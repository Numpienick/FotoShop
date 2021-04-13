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
        [BindProperty, Required(ErrorMessage = "Gelieve een .png of .jpg bestand toe te voegen!")]
        public List<IFormFile> ImageFile { get; set; }

        [BindProperty]
        public Photo NewPhoto { get; set; }

        public IActionResult OnGet()
        {
            using UserRepository repo = new UserRepository(DbUtils.GetDbConnection());
            var acc = repo.GetFromAccount("Account_type", Request.Cookies["UserLoggedIn"]);
            if (acc == null || acc.Account_type != "admin")
            {
                return Redirect("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpload()
        {
            if (!ModelState.IsValid) return Page();

            NewPhoto.Price = NewPhoto.Price.Replace(',', '.');

            if (!float.TryParse(NewPhoto.Price, out var price)) return Page();

            foreach (var item in ImageFile)
            {
                var file = HardDriveUtils.GetFilePath(item);
                var dirPath = HardDriveUtils.GetDirectoryPath(item, NewPhoto.Category_name);

                NewPhoto.Photo_path = $"{NewPhoto.Category_name}/{file}";

                try
                {
                    await using (var memoryStream = new MemoryStream())
                    {
                        await item.CopyToAsync(memoryStream);

                        // Add watermark
                        var watermarkedStream = new MemoryStream();
                        using (var img = Image.FromStream(memoryStream))
                        {
                            using (var graphic = Graphics.FromImage(img))
                            {
                                var font = new Font(FontFamily.GenericSansSerif, (img.Width / 10), FontStyle.Bold, GraphicsUnit.Pixel);
                                var color = Color.FromArgb(155, 255, 255, 255);
                                var brush = new SolidBrush(color);
                                var point = new Point(0, img.Height - (img.Height / 2));

                                graphic.DrawString(" Hoekstra Fotografie ", font, brush, point);
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Page();
                }
            }
            return Redirect("UploadImage");
        }
        //https://edi.wang/post/2018/10/12/add-watermark-to-uploaded-image-aspnet-core
    }
}