using Microsoft.AspNetCore.Identity;

namespace lab.WebApi19Sample.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() { }

        public bool IsActive { get; set; }
    }
}
