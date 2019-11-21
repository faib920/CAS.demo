using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAS
{
    public class MyUserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<MyIdentityUser>
    {
        private IdentityOptions options;

        public MyUserClaimsPrincipalFactory(IOptions<IdentityOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<ClaimsPrincipal> CreateAsync(MyIdentityUser user)
        {
            var userIdentity = new ClaimsIdentity("Identity.Application", options.ClaimsIdentity.UserNameClaimType, options.ClaimsIdentity.RoleClaimType);
            userIdentity.AddClaim(new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id));
            userIdentity.AddClaim(new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName));

            return new ClaimsPrincipal(userIdentity);
        }
    }
}
