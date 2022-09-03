using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class GenderVM {
        [Required]
        public string Name { get; set; }
    }
}
