using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class PermissionKeyAttribute : Attribute, IAsyncAuthorizationFilter, IFilterMetadata
    {
        public PermissionKeyAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole("AgencyManager"))
            {
                if (Key == PermissionKeys.Value1)
                {
                    return;
                }
            }
            else if (context.HttpContext.User.IsInRole("SystemManager"))
            {
                if (Key == PermissionKeys.Value1 || Key == PermissionKeys.Value2 || Key == PermissionKeys.Value3)
                {
                    return;
                }
            }

            context.Result = new StatusCodeResult(403);
        }
    }
}
