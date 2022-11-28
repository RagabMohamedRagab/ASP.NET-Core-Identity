using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class ChangePasswordVM {
        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "اكتب صح يا علق")]
        public string ConfirmPassword { get; set; }
    }
}


