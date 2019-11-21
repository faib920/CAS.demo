using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CAS
{
    public class MyPasswordValidator : IPasswordValidator<MyIdentityUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<MyIdentityUser> manager, MyIdentityUser user, string password)
        {
            return password == user.PasswordHash ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "密码错误" });
        }
    }
}
