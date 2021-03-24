using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop.Classes.Repositories
{
    public class UserRepository : IDisposable
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        //public string GetUserName(string wishlistCode)
        //{
        //    using var connection = _connection;
        //    string user = connection.QuerySingleOrDefault<string>(@"SELECT Name FROM user 
        //    WHERE Wishlist_code = @WishlistCode", new { WishlistCode = wishlistCode });
        //    return user;
        //}

        public bool Create(User user)
        {
            int numRowsAffected = 0;

            using var connection = _connection;
            try
            {
                numRowsAffected = connection.Execute(@"INSERT INTO user (Email, Password, First_name, Last_name) 
             VALUES(@Email, @User_Password, @First_name, @Last_name)", user);
            }
            catch (Exception)
            {
                return numRowsAffected == 1;
            }
            return numRowsAffected == 1;
        }

        public string LogIn(string email, string password)
        {
            using var connection = _connection;
            string id = connection.QuerySingleOrDefault<string>(@"SELECT User_id FROM user 
                WHERE Email = @Email AND Password = @Password",
                new { Email = email, Password = password });
            return id;
        }

        //private bool IsThereAUserWithCode(int code)
        //{
        //    using var connection = DbUtils.GetDbConnection();
        //    var user = connection.QuerySingleOrDefault(@"
        //        SELECT * FROM user WHERE Wishlist_code = @WishlistCode", new { WishlistCode = code });
        //    return user != null;
        //}

        //public string GetWishlistCode(string email, string password)
        //{
        //    using var connection = _connection;
        //    string code = connection.QuerySingleOrDefault<string>(@"SELECT Wishlist_code FROM user 
        //        WHERE Email = @Email AND Password = @Password",
        //        new { Email = email, Password = password });
        //    return code;
        //}
    }
}
