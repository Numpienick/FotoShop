using System.ComponentModel.DataAnnotations;

namespace FotoShop.Classes
{
    public class DBContact
    {
        [Required, MinLength(3), MaxLength(100)]
        public string subject { get; set; }
        
        [Required, MinLength(3), MaxLength(999)]
        public string message { get; set; }
        
        [Required, MinLength(3), MaxLength(45)]
        public string name { get; set; }
        
        [Required, MinLength(3), MaxLength(255)]
        public string email { get; set; }
    }
}