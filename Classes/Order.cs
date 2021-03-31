namespace FotoShop.Classes.Repositories
{
    public class Order
    {
        public string Placed_order_id { get; set; }
        
        public string Account_Id { get; set; }
        
        public string Date_order_placed { get; set; }
        
        public string Date_order_paid { get; set; }
        
        public string Download_link { get; set; }
    }
}