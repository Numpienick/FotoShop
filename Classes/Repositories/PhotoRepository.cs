﻿using Dapper;
using FotoShop.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop.Classes.Repositories
{
    public class PhotoRepository : IDisposable
    {
        private readonly IDbConnection _connection;

        public PhotoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public Photo Get(string id)
        {
            using var connection = _connection;
            Photo photo = connection.QuerySingleOrDefault<Photo>(@"SELECT * FROM photo
            WHERE Photo_id = @Id", new { Id = id });
            return photo;
        }

        public List<Photo> GetList(string category)
        {
            using var connection = _connection;
            List<Photo> photos = connection.Query<Photo>("SELECT * FROM fotoshop.photo WHERE Category_name = @Category",
                new { Category = category }).ToList();
            return photos;
        }

        public Photo Add(Photo photo)
        {
            using var connection = _connection;
            int numRowsAffected = connection.Execute(
<<<<<<< HEAD
<<<<<<< HEAD:Classes/Repositories/PhotoRepository.cs
            "INSERT INTO fotoshop.photo(Photo_id, Photo_path, Price, Description, Category_name) VALUES(@Photo_Path, @Price, @Description, @Category_name)",
            new { Photo_Path = photo.Photo_path, Price = photo.Price, Description = photo.Description, Category_name = photo.Category_name }
=======
            "INSERT INTO fotoshop.photo(Photo_path, Price, Description, Category_name) VALUES(@Photo_Path, @Price, @Description, @CategoryName)",
            new{Photo_Path = photo.Photo_path, Price = photo.Price, Description = photo.Description, CategoryName = photo.Category_name}
>>>>>>> Jelmer:wwwroot/Classes/Repositories/PhotoRepository.cs
=======
            "INSERT INTO fotoshop.photo(Photo_id, Photo_path, Price, Description, Category_name) VALUES(@Photo_Path, @Price, @Description, @Category_name)",
            new { Photo_Path = photo.Photo_path, Price = photo.Price, Description = photo.Description, Category_name = photo.Category_name }
>>>>>>> merging_29_march
            );

            if (numRowsAffected == 1)
            {
                var newPhoto = connection.QuerySingle<Photo>(
                    "SELECT * FROM fotoshop.photo WHERE Photo_id = LAST_INSERT_ID()");
                return newPhoto;
            }
            return null;
        }

        public bool Delete(string photoId)
        {
            using var connection = _connection;
            int numRowsAffected = connection.Execute(
                "DELETE FROM photo WHERE Photo_id = @PhotoID",
                new { PhotoID = photoId });
            return numRowsAffected == 1;
        }

        public bool UpdatePhoto(Photo photo)
        {
            int numRowsAffected = 0;
            using var connection = _connection;
            try
            {
                numRowsAffected = connection.Execute(@"UPDATE photo 
                SET Price = @Price, Description = @Description, Title = @Title
                WHERE Photo_id = @Photo_id", photo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return numRowsAffected == 1;
            }
            return numRowsAffected == 1;
        }

        /// <summary>
        /// Accesses the photo table to retrieve the value of the designated column
        /// </summary>
        /// <param name="toGet">Name of the column that holds the wanted value</param>
        /// <param name="id">Id of the photo</param>
        /// <returns></returns>
        public string GetFromPhoto(string toGet, string id)
        {
            string photoValue = "";
            using var connection = _connection;
            try
            {
                photoValue = connection.ExecuteScalar<string>(@$"SELECT {toGet} FROM photo 
                WHERE Photo_id = @Id", new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return photoValue;
            }
            return photoValue;
        }
    }
}
