using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class GenderVM {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
