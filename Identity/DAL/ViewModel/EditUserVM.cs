using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class EditUserVM {
        public EditUserVM()
        {
            Roles=new List<string>();
            Claims=new List<string>();
        }
        public string Id { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string city { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Claims { get; set; }
    }
}
