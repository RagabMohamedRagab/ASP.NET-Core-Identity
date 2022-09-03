using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.ViewModel {
    public class StudentVM {
       [Display(Name ="First Name")]
       [Required]
        public string FullName { get; set; }
        public int Age { get; set; }
        public FormFile File { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Level { get; set; }
        public string ImgUrl { get; set; }
    }
}
