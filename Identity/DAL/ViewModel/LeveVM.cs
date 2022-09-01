using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class LeveVM {
        [Required]
        public string Name { get; set; }
    }
}
