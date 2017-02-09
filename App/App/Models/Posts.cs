using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Posts
    {
        [Key]
        public int PostID { get; set; }

        [Required(ErrorMessage = "Povinný údaj!")]
        [Display(Name = "Obsah")]
        public string Content { get; set; }

        [Required]
        public int ThemeID { get; set; }

        [Required]
        [Display(Name = "Přidal")]
        public string AddedBy { get; set; }

        [Display(Name = "Datum přidání")]
        public DateTime AddedAt { get; set; }
    }
}