using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Themes
    {
        [Key]
        public int ThemeID { get; set; }

        [Required(ErrorMessage = "Povinný údaj!")]
        [Display(Name = "Název")]
        public string ThemeName { get; set; }

        [Display(Name = "Popis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Založil")]
        public string CreatedBy { get; set; }

        [Display(Name = "Datum založení")]
        public DateTime CreatedAt { get; set; }
    }
}