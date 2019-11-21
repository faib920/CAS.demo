using IdentityModel.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api
{
    /// <summary>
    /// 调用微服务的客户端。
    /// </summary>
    public abstract class Client
    {
        public abstract string ClientId { get; }

        public abstract string ClientSecret { get; }

        public async Task<string> Invoke(string data)
        {
            var httpClient = new HttpClient();

            //连接CAS发现终结点
            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:32865");

            //客户端凭证验证
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                });

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }

            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "data", data } });

            //调用微服务，目前两个模拟的微服务都用同一个地址
            var response = await httpClient.PostAsync("http://localhost:7431/microapi", content);

            return await response.Content.ReadAsStringAsync();
        }
    }

    public class Client1 : Client
    {
        public override string ClientId => "api1";
        public override string ClientSecret => "api1_secret";
    }

    public class Client2 : Client
    {
        public override string ClientId => "api2";
        public override string ClientSecret => "api2_secret";
    }
}
