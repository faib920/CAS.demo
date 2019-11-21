using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace CAS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>(); 

            //使用这个会添加cookie
            //services.AddIdentity<IdentityUser, IdentityRole>();

            services.TryAddScoped<SignInManager<MyIdentityUser>>();
            services.TryAddScoped<UserManager<MyIdentityUser>>();
            services.TryAddScoped<IPasswordValidator<MyIdentityUser>, MyPasswordValidator>();
            services.TryAddScoped<IPasswordHasher<MyIdentityUser>, MyPasswordHasher>();
            services.TryAddScoped<IUserStore<MyIdentityUser>, MyUserStore>();
            services.TryAddScoped<IUserPasswordStore<MyIdentityUser>, MyUserStore>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<MyIdentityUser>, MyUserClaimsPrincipalFactory>();
            services.TryAddScoped<IUserValidator<MyIdentityUser>, UserValidator<MyIdentityUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IdentityErrorDescriber>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.Run(async (context) =>
            {
                //登录
                if (context.Request.Path.Equals("/login"))
                {
                    var userName = context.Request.Form["userName"];
                    var password = context.Request.Form["password"];

                    var httpClient = new HttpClient();

                    //连接CAS发现终结点
                    var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:32865");

                    //资源所有者密码验证
                    var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                        {
                            Address = disco.TokenEndpoint, 
                            UserName = userName, 
                            Password = password, 
                            ClientId = "auth",
                            ClientSecret = "auth_secret",
                            GrantType = GrantType.ResourceOwnerPassword
                        });

                    if (tokenResponse.IsError)
                    {
                        await context.Response.WriteAsync(tokenResponse.ErrorDescription ?? tokenResponse.Error);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(tokenResponse.Raw);
                    }
                }
                //刷新token
                else if (context.Request.Path.Equals("/refresh"))
                {
                    var token = context.Request.Form["token"];

                    var httpClient = new HttpClient();

                    //连接CAS发现终结点
                    var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:32865");

                    //资源所有者密码验证
                    var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
                        {
                            Address = disco.TokenEndpoint,
                            RefreshToken = token,
                            ClientId = "auth",
                            ClientSecret = "auth_secret"
                        });

                    if (tokenResponse.IsError)
                    {
                        await context.Response.WriteAsync(tokenResponse.ErrorDescription ?? tokenResponse.Error);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(tokenResponse.Raw);
                    }
                }
                else
                {
                    await context.Response.WriteAsync("Hello World!");
                }
            });
        }
    }
}
