using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class CreateRoleVM {
        [Required(ErrorMessage ="Required Role Name !!")]
        public string RoleName { get; set; }
    }
}
