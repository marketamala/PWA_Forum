using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Responses
    {
        [Key]
        public int ResponseID { get; set; }

        [Required(ErrorMessage = "Povinný údaj!")]
        [Display(Name = "Obsah")]
        public string RContent { get; set; }

        [Required]
        public int RootID { get; set; }

        [Required]
        [Display(Name = "Přidal")]
        public string AddedBy { get; set; }

        [Display(Name = "Datum přidání")]
        public DateTime AddedAt { get; set; }
    }
}