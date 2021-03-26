using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace FotoShop.wwwroot.Classes.Repositories
{
    public class PhotoRepository
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;
                                        Database=fotoshop;
                                        Uid=root;
                                        Pwd=admin;";
            return new MySqlConnection(connectionString);
        }

        public List<Photo> GetList(string category)
        {
            using var connection = Connect();
            List<Photo> photos = connection.Query<Photo>("SELECT * FROM fotoshop.photo WHERE Category_name = @Category",
                new{Category = category}).ToList();
            return photos;
        }

        public Photo Add(Photo photo)
        {
            using var connection = Connect();
            int numRowsAffected = connection.Execute(
            "INSERT INTO fotoshop.photo(Photo_path, Price, Description, Category_name) VALUES(@Photo_Path, @Price, @Description, @CategoryName)",
            new{Photo_Path = photo.Photo_path, Price = photo.Price, Description = photo.Description, CategoryName = photo.Category_name}
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
            using var connection = Connect();
            int numRowsAffected = connection.Execute(
                "DELETE FROM fotoshop.photo WHERE Photo_id = @PhotoID",
                new {PhotoID = photoId});
            if (numRowsAffected == 1)
            {
                return true;
            }

            return false;
        }

        public void ChangePrice(int photoId, int price)
        {
            using var connection = Connect();
            var numRowsAffected = connection.Execute(
                "UPDATE fotoshop.photo SET Price = @Price WHERE Photo_id = @PhotoID",
                new{Price = price,PhotoID = photoId});
        }
        
    }
}