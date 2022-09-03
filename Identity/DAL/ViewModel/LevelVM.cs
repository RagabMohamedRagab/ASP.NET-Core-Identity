using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class LevelVM {
        [Required]
        public string Name { get; set; }
    }
}
