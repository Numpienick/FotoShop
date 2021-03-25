using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop.Classes
{
    public static class DbUtils
    {
        public static IDbConnection GetDbConnection()
        {
            return new MySqlConnection("Server=127.0.0.1;Port=3306;Database=fotoshop;Uid=root;Pwd=admin;Allow User Variables=True");
        }
    }
}
