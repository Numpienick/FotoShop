using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop.Classes
{
    public class UpdateUserInfo
    {
        [Required(ErrorMessage = "U moet een e-mail adres invullen"),
            MinLength(4, ErrorMessage = "Uw e-mail moet uit minimaal 4 karakters bestaan"),
            DataType(DataType.EmailAddress),
            MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "U moet een voornaam invullen"),
            MinLength(2, ErrorMessage = "Uw naam moet uit minimaal 2 karakters bestaan"),
            MaxLength(45)]
        public string First_name { get; set; }

        [Required(ErrorMessage = "U moet een achternaam invullen"),
            MinLength(2, ErrorMessage = "Uw naam moet uit minimaal 2 karakters bestaan"),
            MaxLength(45)]
        public string Last_name { get; set; }

        [MinLength(2, ErrorMessage = "Uw wachtwoord moet uit minimaal 2 karakters bestaan"),
            MaxLength(45),
            DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(45),
            DataType(DataType.Password),
            Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string PassConfirm { get; set; }

        [Required(ErrorMessage = "Voer uw huidige wachtwoord in"),
            MinLength(2, ErrorMessage = "Uw wachtwoord moet uit minimaal 2 karakters bestaan"),
            MaxLength(45),
            DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
