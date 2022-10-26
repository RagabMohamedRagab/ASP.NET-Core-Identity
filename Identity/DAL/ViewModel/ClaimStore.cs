using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.DAL.ViewModel {
    //Claim : Name value pair that resprsentd what subject is it?
    public class ClaimStore {
        public static List<Claim> Claims = new List<Claim>() {
          new Claim("Create Role","Create Role"),
          new Claim("Edit Role","Edit Role"),
          new Claim("Delete Role","Delete Role")
        };
    }
}
