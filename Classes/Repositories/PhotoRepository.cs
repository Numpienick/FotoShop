using Dapper;
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
            "INSERT INTO fotoshop.photo(Photo_id, Photo_path, Price, Description, Category_name) VALUES(@Photo_Path, @Price, @Description, @Category_name)",
            new { Photo_Path = photo.Photo_path, Price = photo.Price, Description = photo.Description, Category_name = photo.Category_name }
            );

            if (numRowsAffected == 1)
            {
                var newPhoto = connection.QuerySingle<Photo>(
                    "SELECT * FROM fotoshop.photo WHERE Photo_id = LAST_INSERT_ID()");
                return newPhoto;
            }
            return null;
        }

        public bool Delete(int photoId)
        {
            using var connection = _connection;
            int numRowsAffected = connection.Execute(
                "DELETE FROM fotoshop.photo WHERE Photo_id = @PhotoID",
                new { PhotoID = photoId });
            if (numRowsAffected == 1)
            {
                return true;
            }

            return false;
        }

        public void ChangePrice(int photoId, int price)
        {
            using var connection = _connection;
            var numRowsAffected = connection.Execute(
                "UPDATE fotoshop.photo SET Price = @Price WHERE Photo_id = @PhotoID",
                new { Price = price, PhotoID = photoId });
        }

        public string GetPhotoPath(string id)
        {
            using var connection = _connection;
            string path = connection.QuerySingleOrDefault<string>(@"SELECT Photo_path FROM photo 
            WHERE Photo_id = @Id", new { id });
            return path;
        }
    }
}
