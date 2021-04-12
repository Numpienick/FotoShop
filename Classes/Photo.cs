using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FotoShop.Classes
{
    public class Photo
    {
        public string Photo_id { get; set; }

        public string Photo_path { get; set; }

        [Required(ErrorMessage = "Gelieve een titel in te voeren!"), MaxLength(45, ErrorMessage = "Uw titel moet uit minder dan 45 tekens bestaan")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Gelieve een beschrijving in te voeren!"), MaxLength(500, ErrorMessage = "Uw beschrijving moet uit minder dan 500 tekens bestaan")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Gelieve een prijs in te voeren!"),
         Range(0, float.MaxValue, ErrorMessage = "Uw prijs moet een geldig decimaal getal zijn"),
         RegularExpression(@"[\d]{1,3}([,.][\d]{1,2})?", ErrorMessage = "Uw prijs is te groot. Maximale prijs is 999,99")]
        public string Price { get; set; }

        public string Category_name { get; set; }
    }
}