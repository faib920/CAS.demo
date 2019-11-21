using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:32865";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(0);//滑动时间
                    options.Audience = "micro_scope";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.Run(async (context) =>
            {
                //模拟微服务，通过客户端凭证验证
                if (context.Request.Path.Equals("/microapi"))
                {
                    if (context.User.Identity.IsAuthenticated)
                    {
                        var data = context.Request.Form["data"];
                        await context.Response.WriteAsync("Hello " + data + "!");
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
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
