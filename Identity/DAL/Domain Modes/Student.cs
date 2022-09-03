using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.DAL.Domain_Modes {
    public class Student {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must Be Insert")]
        public string Name { get; set; }
        public int Age { get; set; }
        public string ImgUrl { get; set; }
        [ForeignKey(nameof(Gender))]
        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        [ForeignKey(nameof(Level))]
        public int? StudentId { get; set; }
        public virtual Level Level { get; set; }

    }
}
