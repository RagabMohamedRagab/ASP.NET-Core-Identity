using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class LevelVM {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
