using Microsoft.AspNetCore.Identity;

namespace CAS
{
    public class MyIdentityUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
