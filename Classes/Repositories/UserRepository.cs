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
            try
            {
                numRowsAffected = _connection.Execute(@"INSERT INTO account (Email, Password, First_name, Last_name) 
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
            string id = _connection.QuerySingleOrDefault<string>(@"SELECT Account_id FROM account 
                WHERE Email = @Email AND Password = @Password",
                new { Email = email, Password = password });
            return id;
        }

        /// <summary>
        /// Accesses the account table to retrieve the value(s) of the designated column
        /// </summary>
        /// <param name="toGet">Name of the column(s) that holds the wanted value</param>
        /// <param name="id">Id of the account</param>
        /// <returns></returns>
        public User GetFromAccount(string toGet, string id)
        {
            User user = new User();            
            try
            {
                user = _connection.QuerySingleOrDefault<User>(@$"SELECT {toGet} FROM account 
                WHERE Account_id = @Id", new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return user;
            }
            return user;
        }

        /// <summary>
        /// Updates the designated value(s) of the account with the corresponding id
        /// </summary>
        /// <param name="toUpdate">Use the following format:
        /// "ColumnName1='Value1', ColumnName2='Value2'"</param>
        /// <param name="id">Id of the account</param>
        public bool Update(string toUpdate, string id)
        {
            int numRowsAffected = 0;
            try
            {
                numRowsAffected = _connection.Execute(@$"UPDATE account SET {toUpdate}
                WHERE Account_id = @Id", new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return numRowsAffected == 1;
            }
            return numRowsAffected == 1;
        }
    }
}
