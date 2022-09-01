using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.DAL.Domain_Modes {
    public class Level {
        public int  Id { get; set; }
        [Required]
        public string  Name { get; set; }
      public virtual ICollection<Student> Students { get; set; }    
    }
}
