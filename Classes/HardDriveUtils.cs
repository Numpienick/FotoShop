﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FotoShop.Classes
{
    public class HardDriveUtils
    {
        public static string GetFilePath(IFormFile imageFile)
        {
            if(imageFile != null)
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
            string dirPath = Path.Combine(imagesDir,categoryName, filepath);
            return dirPath;
        }

        public static string GetImageDirectory()
        {
            string imagesDir = Path.Combine(new DirectoryInfo(
                Directory.GetCurrentDirectory()).FullName, "wwwroot", "Images", "ProductImages");
            return imagesDir;
        }

        public static bool DeleteImage(string pathImagesDir, string photoPath)
        {
            var imagePath = Path.Combine(pathImagesDir, photoPath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return true;
            }

            return false;
        }
    }
}