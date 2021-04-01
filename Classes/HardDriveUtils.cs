using System.IO;
using System.Threading.Tasks;
using FotoShop.Classes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Classes
{
    public class HardDriveUtils
    {
        public static string GetFilePath(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);
                string file = fileName + extension;
                return file;
            }
            return null;
        }

        public static string GetDirectoryPath(IFormFile imageFile, string categoryName)
        {
            string imagesDir = GetImageDirectory();
            string filepath = GetFilePath(imageFile);
            string dirPath = Path.Combine(imagesDir, categoryName, filepath);
            return dirPath;
        }

        public static string GetImageDirectory()
        {
            string imagesDir = Path.Combine(new DirectoryInfo(
                Directory.GetCurrentDirectory()).FullName, "wwwroot", "Images", "ProductImages");
            return imagesDir;
        }


        /// <summary>
        /// Deletes photo from the database and from the folder
        /// </summary>
        /// <param name="id">Id of the photo in the database</param>
        public static void DeleteImage(string id)
        {
            var imagesDir = GetImageDirectory();

            using PhotoRepository repo = new PhotoRepository(DbUtils.GetDbConnection());
            string photoPath = repo.GetFromPhoto("Photo_path", id);

            using PhotoRepository delRepo = new PhotoRepository(DbUtils.GetDbConnection());
            delRepo.Delete(id);

            var imagePath = Path.Combine(imagesDir, photoPath);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}