using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CAS
{
    public class MyUserStore : IUserPasswordStore<MyIdentityUser>
    {
        private List<MyIdentityUser> users = new List<MyIdentityUser>()
        {
            new MyIdentityUser { Id = "test1", UserName = "测试用户1", PasswordHash = "123", Role = "SystemManager" },
            new MyIdentityUser { Id = "test2", UserName = "测试用户2", PasswordHash = "123", Role = "BranchManager" },
            new MyIdentityUser { Id = "test3", UserName = "测试用户3", PasswordHash = "123", Role = "AgencyManager" },
            new MyIdentityUser { Id = "test4", UserName = "测试用户4", PasswordHash = "123" },
        };

        public Task<IdentityResult> CreateAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<MyIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return users.FirstOrDefault(s => s.Id == userId);
        }

        public async Task<MyIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return users.FirstOrDefault(s => s.UserName == normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return user.PasswordHash;
        }

        public async Task<string> GetUserIdAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return user.Id;
        }

        public Task<string> GetUserNameAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            return user.PasswordHash != null;
        }

        public Task SetNormalizedUserNameAsync(MyIdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(MyIdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(MyIdentityUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(MyIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
