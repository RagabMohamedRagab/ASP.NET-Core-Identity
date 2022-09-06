using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class StudentVM {
        public int Id { get; set; }
        [Display(Name ="Full Name")]
       [Required]
        public string FullName { get; set; }
        public int Age { get; set; }
        public IFormFile File { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Level { get; set; }
        public string ImgUrl { get; set; }
    }
}
