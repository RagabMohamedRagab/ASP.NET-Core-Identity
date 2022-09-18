using System.ComponentModel.DataAnnotations;

namespace Identity.Utilties {
    public class ValidEmailDomain:ValidationAttribute {  
          private readonly string _allowDomain;
           public ValidEmailDomain (string allowDomain)
            {
            _allowDomain= allowDomain;
            }
       public override bool IsValid(object value)
        {
            string [] domain = value.ToString().Split("@");
            return domain[1].ToUpper() == _allowDomain.ToLower();
        }
    }
}
