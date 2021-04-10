using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop.Classes
{
    public class User
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

        [Required(ErrorMessage = "U moet een wachtwoord invullen"),
            MinLength(2, ErrorMessage = "Uw wachtwoord moet uit minimaal 2 karakters bestaan"),
            MaxLength(45),
            DataType(DataType.Password)]
        public string User_Password { get; set; }

        [Required(ErrorMessage = "Wachtwoorden komen niet overeen"),
            MaxLength(45),
            DataType(DataType.Password),
            Compare("User_Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string User_PassConfirm { get; set; }
    }
}
