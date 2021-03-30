using System.ComponentModel.DataAnnotations;

namespace FotoShop.Classes
{
    public class DBContact
    {
        [Required(ErrorMessage = "Gelieve een geldig onderwerp in te voeren!"), MinLength(3), MaxLength(100)]
        public string subject { get; set; }
        
        [Required(ErrorMessage = "Gelieve een geldig bericht in te voeren!"), MinLength(3), MaxLength(999)]
        public string message { get; set; }
        
        [Required(ErrorMessage = "Gelieve een geldige naam in te voeren!"), MinLength(3), MaxLength(45)]
        public string name { get; set; }
        
        [Required(ErrorMessage = "Gelieve een geldig e-mail adres in te voeren!"), MinLength(3), MaxLength(255)]
        public string email { get; set; }
    }
}
