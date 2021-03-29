using System.Data;
using MySql.Data.MySqlClient;

namespace FotoShop.wwwroot.Classes.Repositories
{
    public class CategoryRepository
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;
                                        Database=fotoshop;
                                        Uid=root;
                                        Pwd=admin;";
            return new MySqlConnection(connectionString);
        }
        
    }
}