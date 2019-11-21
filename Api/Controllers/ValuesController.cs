using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "SystemManager,AgencyManager")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [PermissionKey(PermissionKeys.Value1)]
        public async Task<ActionResult<IEnumerable<string>>> Value1()
        {
            //模拟调用两个微服务客户端，返回数据
            var str1 = await new Client1().Invoke(User.Identity.Name + " 来自 client1");
            var str2 = await new Client2().Invoke(User.Identity.Name + " 来自 client2");

            return new string[] { str1, str2 };
        }

        [HttpGet]
        [PermissionKey(PermissionKeys.Value2)]
        public async Task<ActionResult<IEnumerable<string>>> Value2()
        {
            //模拟调用两个微服务客户端，返回数据
            var str1 = await new Client1().Invoke(User.Identity.Name + " 来自 client1");
            var str2 = await new Client2().Invoke(User.Identity.Name + " 来自 client2");

            return new string[] { str1, str2 };
        }

        [HttpGet]
        [PermissionKey(PermissionKeys.Value3)]
        public async Task<ActionResult<IEnumerable<string>>> Value3()
        {
            //模拟调用两个微服务客户端，返回数据
            var str1 = await new Client1().Invoke(User.Identity.Name + " 来自 client1");
            var str2 = await new Client2().Invoke(User.Identity.Name + " 来自 client2");

            return new string[] { str1, str2 };
        }
    }
}
