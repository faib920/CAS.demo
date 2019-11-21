using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace CAS
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("auth_scope", "Identity API"),
                new ApiResource("micro_scope", "Micro API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId ="api1",
                    ClientName = "api1-client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("api1_secret".Sha256())},
                    AccessTokenLifetime = 30, //token过期时间
                    AllowedScopes = {
                        "micro_scope"
                    }
                },
                new Client
                {
                    ClientId ="api2",
                    ClientName = "api2-client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("api2_secret".Sha256())},
                    AccessTokenLifetime = 30, //token过期时间
                    AllowedScopes = {
                        "micro_scope"
                    }
                },
                new Client
                {
                    ClientId ="auth",
                    ClientName = "auth-client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("auth_secret".Sha256())},
                    AccessTokenLifetime = 30, //token过期时间
                    //AbsoluteRefreshTokenLifetime = 2592000,
                    //RefreshTokenExpiration = TokenExpiration.Sliding,
                    //SlidingRefreshTokenLifetime = 3600,
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        "auth_scope"
                    }
                }
            };
        }
    }
}
