using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class LoginVM {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }
        [Display(Name = "Remmber Me")]
        public bool RemmberMe { get; set; }
    }
}




