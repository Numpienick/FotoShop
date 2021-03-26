using Dapper;
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

        public string GetPhotoPath(string id)
        {
            using var connection = _connection;
            string path = connection.QuerySingleOrDefault<string>(@"SELECT Photo_path FROM photo 
            WHERE Photo_id = @Id", new { id });
            return path;
        }
    }
}
