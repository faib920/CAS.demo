using Microsoft.AspNetCore.Identity;

namespace CAS
{
    public class MyPasswordHasher : IPasswordHasher<MyIdentityUser>
    {
        public string HashPassword(MyIdentityUser user, string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(MyIdentityUser user, string hashedPassword, string providedPassword)
        {
            return hashedPassword == providedPassword ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
