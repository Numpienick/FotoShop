using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FotoShop.Classes
{
    public class Photo
    {
        public string Photo_id { get; set; }
        public string Photo_path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public string Price { get; set; }
        public string Category_name { get; set; }
    }
}