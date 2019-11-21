using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAS
{
    /// <summary>
    /// 资源所有者密码验证器。集成了 Identity。
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private SignInManager<MyIdentityUser> signInManager;
        private IdentityOptions identityOptions;

        public ResourceOwnerPasswordValidator(SignInManager<MyIdentityUser> signInManager, IOptions<IdentityOptions> identityOptions)
        {
            this.signInManager = signInManager;
            this.identityOptions = identityOptions.Value;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await signInManager.UserManager.FindByIdAsync(context.UserName);

            //如果使用signInManager.PasswordSignInAsync(user, context.Password, false, false)则会生成cookie
            if (user != null && (await signInManager.CheckPasswordSignInAsync(user, context.Password, true)).Succeeded)
            {
                context.Result = new GrantValidationResult(
                     subject: context.UserName,
                     authenticationMethod: "custom",
                     claims: GetUserClaims(user));
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "错误的用户名及密码");
            }
        }

        //可以根据需要设置相应的Claim
        //例如：角色，权限列表等
        private Claim[] GetUserClaims(MyIdentityUser user)
        {
            return new Claim[]
                {
                    new Claim(identityOptions.ClaimsIdentity.UserIdClaimType, user.Id),
                    new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName),
                    new Claim(identityOptions.ClaimsIdentity.RoleClaimType, user.Role),
                    new Claim(identityOptions.ClaimsIdentity.RoleClaimType, user.Role + "1")
                };
        }
    }
}
