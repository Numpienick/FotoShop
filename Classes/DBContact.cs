using System.ComponentModel.DataAnnotations;

namespace FotoShop.Classes
{
    public class DBContact
    {
        [Required(ErrorMessage = "Gelieve een geldig onderwerp in te voeren!"), MinLength(3, ErrorMessage = "Uw onderwerp moet uit minimaal 3 tekens bestaan"), MaxLength(100)]
        public string subject { get; set; } = "";

        [Required(ErrorMessage = "Gelieve een geldig bericht in te voeren!"), MinLength(3, ErrorMessage = "Uw bericht moet uit minimaal 3 tekens bestaan"), MaxLength(999)]
        public string message { get; set; } = "";

        [Required(ErrorMessage = "Gelieve een geldige naam in te voeren!"), MinLength(3, ErrorMessage = "Uw naam moet uit minimaal 3 tekens bestaan"), MaxLength(45)]
        public string name { get; set; } = "";

        [Required(ErrorMessage = "Gelieve een geldig e-mail adres in te voeren!"), MinLength(3, ErrorMessage = "Uw e-mail moet uit minimaal 3 tekens bestaan"), MaxLength(255)]
        public string email { get; set; } = "";
    }
}
