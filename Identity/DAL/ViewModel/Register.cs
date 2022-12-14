using Identity.Utilties;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class Register {
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse",controller: "Account")]
        [ValidEmailDomain(allowDomain:"ragab.rego",ErrorMessage = "Your Domain Must Be ragab.rego")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Not Equal")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
