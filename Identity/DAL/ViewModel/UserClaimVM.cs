using System.Collections.Generic;

namespace Identity.DAL.ViewModel {
    public class UserClaimVM {
        public UserClaimVM()
        {
            Claims = new List<UserClaim>();
        }
        public string  userId { get; set; }
        public IList<UserClaim> Claims { get; set; }
    }
}
