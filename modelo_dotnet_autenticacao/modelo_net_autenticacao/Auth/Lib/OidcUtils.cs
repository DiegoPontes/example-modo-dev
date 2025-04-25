using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace modelo_net_autenticacao.Auth
{
    /// <summary>
    /// Classe com métodos auxiliares para obter informações do provedor de identidade.
    /// </summary>
    public static class OidcUtils
    {
        public static async Task<string> GetString(string address, string proxy, string proxyUser, string proxyPass, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                // Se necessário (máquina de desenvolvedor), configura o proxy que será utilizado
                if (!string.IsNullOrEmpty(proxy))
                {
                    try
                    {
                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(proxy);
                        if (!string.IsNullOrEmpty(proxyUser))
                        {
                            handler.Proxy.Credentials = new NetworkCredential(proxyUser, proxyPass);
                        }
                        client.Timeout = TimeSpan.FromSeconds(30);
                        client.DefaultRequestHeaders.ProxyAuthorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    }
                    catch (Exception) { }
                }

                try
                {
                    HttpResponseMessage response = await client.GetAsync(address);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                // TODO: Tratar eventuais exceções
                catch (Exception)
                {
                }
            }

            return string.Empty;
        }

        public static async Task<byte[]> GetPicture(string address, string proxy, string proxyUser, string proxyPass, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                // Se necessário (máquina de desenvolvedor), configura o proxy que será utilizado
                if (!string.IsNullOrEmpty(proxy))
                {
                    try
                    {
                        HttpClientHandler handler = new HttpClientHandler();
                        handler.Proxy = new WebProxy(proxy);
                        if (!string.IsNullOrEmpty(proxyUser))
                        {
                            handler.Proxy.Credentials = new NetworkCredential(proxyUser, proxyPass);
                        }
                        client.Timeout = TimeSpan.FromSeconds(30);
                        client.DefaultRequestHeaders.ProxyAuthorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    }
                    catch (Exception) { }
                }

                try
                {
                    HttpResponseMessage response = await client.GetAsync(address);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsByteArrayAsync();
                }
                // TODO: Tratar eventuais exceções
                catch (Exception)
                {
                }
            }

            return Array.Empty<byte>();
        }
    }
}
