using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class Roles {
        [Display(Name="Name")]
        public string RoleName { get; set; }
    }
}
