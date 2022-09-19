using Microsoft.AspNetCore.Identity;

namespace Identity.DAL.ViewModel {
    public class ApplicationUser:IdentityUser {
        public string City { get; set; }
    }
}
