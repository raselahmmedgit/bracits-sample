using Microsoft.AspNetCore.Identity;

namespace lab.WebApi19Sample.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public int Age { get; set; }
    }

}
