using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace modelo_net_autenticacao.Auth
{
    public class OidcConfigOptions
    {
        private readonly string _prefix;
        public OidcConfigOptions(String prefix)
        {
            _prefix = prefix;
        }

        public string Type => _prefix;
        public string ClientId => ConfigurationManager.AppSettings[$"{_prefix}:ClientId"];
        public string ClientSecret => ConfigurationManager.AppSettings[$"{_prefix}:ClientSecret"];
        public string Authority => ConfigurationManager.AppSettings[$"{_prefix}:Authority"];
        public string RedirectUri => ConfigurationManager.AppSettings[$"{_prefix}:RedirectUri"];
        public string LogoutUri => ConfigurationManager.AppSettings[$"{_prefix}:LogoutUri"];
        public string ClaimsKey => ConfigurationManager.AppSettings[$"{_prefix}:ClaimsKey"];

        /// <summary>
        /// A lista de escopos do IDP podem ser encontradas em:
        /// https://rhsso.idp-hml.sp.gov.br/auth/realms/idpsp/.well-known/openid-configuration
        /// A lista de lista escopos do Gov.Br em:
        /// https://sso.acesso.gov.br/.well-known/openid-configuration
        /// </summary>
        public string Scopes => ConfigurationManager.AppSettings[$"{_prefix}:Scopes"];
        public string ApiUrl => ConfigurationManager.AppSettings[$"{_prefix}:ApiUrl"];
    }
}
