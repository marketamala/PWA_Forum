using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Accounts
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Povinný údaj!")]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; }

        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Povinný údaj!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Povinný údaj!")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hesla se musí shodovat!")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        public string ConfirmPasswd { get; set; }

        [Required]
        [Display(Name = "Pohlaví")]
        public bool IsMale { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Země")]
        public string Country { get; set; }

        [Display(Name = "Město")]
        public string City { get; set; }

        [Display(Name = "Věk")]
        public string Age { get; set; }

        [Display(Name = "Zájmy")]
        public string Interests { get; set; }

        [Display(Name = "Další informace")]
        public string Info { get; set; }
    }
}