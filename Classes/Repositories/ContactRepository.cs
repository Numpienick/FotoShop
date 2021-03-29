using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace FotoShop.Classes.Repositories
{
    public class ContactRepository
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;
                                        Database=fotoshop;
                                        Uid=root;
                                        Pwd=admin;";
            return new MySqlConnection(connectionString);
        }

        public bool InsertNewContact(DBContact dbContact)
        {
            using var connection = Connect();
            var numRowEffected = connection.Execute(
                @"INSERT INTO contact(Subject, Message, Name, Email)
                        VALUES (@paraSubject, @paraMessage, @paraName, @paraEmail)", 
                    new { paraSubject = dbContact.subject, 
                                paraMessage = dbContact.message,
                                paraName = dbContact.name,
                                paraEmail = dbContact.email
            });
            return numRowEffected == 1;
        }
        
    }
}