using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class RestPasswordVM {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="اكتب صح يا علق")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Token { get; set; }
    }
}
