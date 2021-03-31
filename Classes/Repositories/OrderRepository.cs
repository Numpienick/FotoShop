using System;
using System.Data;
using Dapper;

namespace FotoShop.Classes.Repositories
{
    public class OrderRepository: IDisposable
    {
        private readonly IDbConnection _connection;

        public OrderRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public Order Get(string AccountId)
        {
            using var connection = _connection;
            Order order = connection.QuerySingleOrDefault<Order>(@"SELECT * FROM placed_order WHERE Account_id = @AccountId", new { AccountId = AccountId });
            return order; 
        }
        
        public bool Add(string AccountId, string Downloadlink, string PhotoId)
        {
            using var connection = _connection;
            int NrOfRowsEffected = connection.Execute(
                @"INSERT INTO placed_order(Account_id, Download_link) VALUES (@Accountid, @Downloadlink)", new
                {
                    AccountId = AccountId, Downloadlink = Downloadlink
                });
            SelectOrderID(AccountId, PhotoId);
            return NrOfRowsEffected == 1;
        }

        public void SelectOrderID(string AccountID, string PhotoId)
        {
            using var connection = _connection;
            Order Neworder = connection.QuerySingleOrDefault<Order>(
                @"SELECT * FROM placed_order WHERE Account_id = @AccountId", new
                {
                    AccountId = AccountID
                });
            var Orderid = Neworder.Placed_order_id;
            InsertPhoto(Orderid,PhotoId);
        }

        public void InsertPhoto(string orderid, string photoid)
        {
            using var connection = _connection;
            int NrOfRowsEffected = connection.Execute(@"
            INSERT INTO placed_order_photo(Placed_order_id, Photo_id) VALUES(@orderid,@photoid)",
                new {orderid = orderid, photoid = photoid});
        }
    }
}