using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FotoShop.Classes
{
    public class LogIn
    {
        [Required(ErrorMessage = "U moet een e-mail adres invullen."),
            MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "U moet een wachtwoord invullen."),
            MaxLength(45),
            DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
