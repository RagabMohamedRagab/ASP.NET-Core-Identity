﻿using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class Register {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Not Equal")]
        public string ConfirmPassword { get; set; }
    }
}