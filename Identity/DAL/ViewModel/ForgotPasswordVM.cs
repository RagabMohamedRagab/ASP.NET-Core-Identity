using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class ForgotPasswordVM {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
