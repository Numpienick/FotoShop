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
            string id = connection.QuerySingleOrDefault<string>(@"SELECT Account_id FROM account 
                WHERE Email = @Email AND Password = @Password",
                new { Email = email, Password = password });
            return id;
        }

        /// <summary>
        /// Accesses the account table to retrieve the value of the designated column
        /// </summary>
        /// <param name="toGet">Name of the column that holds the wanted value</param>
        /// <param name="id">Id of the user</param>
        /// <returns></returns>
        public string GetFromAccount(string toGet, string id)
        {
            string userValue = "";
            using var connection = _connection;
            try
            {
                userValue = connection.ExecuteScalar<string>(@$"SELECT {toGet} FROM account 
                WHERE Account_id = @Id", new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return userValue;
            }
            return userValue;
        }
    }
}
