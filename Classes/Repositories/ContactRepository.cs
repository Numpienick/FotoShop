using System;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace FotoShop.Classes.Repositories
{
    public class ContactRepository : IDisposable
    {
        private readonly IDbConnection _connection;
        public ContactRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public bool InsertNewContact(DBContact dbContact)
        {
            var numRowEffected = _connection.Execute(
                @"INSERT INTO fotoshop.contact(Subject, Message, Name, Email)
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