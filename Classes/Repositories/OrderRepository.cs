using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Ubiety.Dns.Core;

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

        public Order Get(string Order)
        {
            using var connection = _connection;
            Order order = connection.QuerySingleOrDefault<Order>(@"SELECT * FROM placed_order WHERE Placed_order_id = @Order_id", new { Order_id = Order });
            return order; 
        }
        
        public Order Add(string AccountId, string Downloadlink, string PhotoId)
        {
            using var connection = _connection;
            Order newOrder = connection.QuerySingle<Order>(
                @"INSERT INTO placed_order(Account_id, Download_link) VALUES (@Accountid, @Downloadlink);
                SELECT * FROM placed_order WHERE Placed_order_id = LAST_INSERT_ID()", new
                {
                    AccountId = AccountId, Downloadlink = Downloadlink
                });
            InsertPhoto(newOrder.Placed_order_id, PhotoId);
            return newOrder;
        }
        
        public void InsertPhoto(string orderid, string photoid)
        {
            using var connection = _connection;
            int NrOfRowsEffected = connection.Execute(@"
            INSERT INTO placed_order_photo(Placed_order_id, Photo_id) VALUES(@orderid,@photoid)",
                new {orderid = orderid, photoid = photoid});
        }

        public Order CheckFoto(string orderid, string photoid)
        {
            using var connection = _connection;
            Order order = connection.QuerySingleOrDefault<Order>(
                @"SELECT * FROM placed_order_photo WHERE Placed_order_id = @orderid AND Photo_id = @photoid",
                new {orderid = orderid, photoid = photoid});
            return order;
        }
        
        public List<int> GetPhoto(string Order_id)
        {
            using var connection = _connection;
            List<int> AllPhoto =
                connection.Query<int>(@"SELECT Photo_id FROM placed_order_photo WHERE Placed_order_id = @Order_id",
                    new {Order_id = Order_id}).ToList();
            return AllPhoto;
        }

        public void OrderSucces(string OrderCookie)
        {
            using var connection = _connection;
            var OrderSucces = connection.Execute(@"DELETE FROM placed_order_photo WHERE Placed_order_id = @OrderCookie",
                new {OrderCookie = OrderCookie});
        }
        
        public void DeletePhoto(string Photo_id, string OrderCookie)
        {
            using var connection = _connection;
            var DeletePhoto = connection.Execute("DELETE FROM placed_order_photo WHERE Photo_id = @Photo_id AND Placed_order_id = @OrderCookie",
                new {Photo_id = Photo_id, OrderCookie = OrderCookie});
        }
    }
}