using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.WsFederation;
using modelo_net_autenticacao.Auth.SefazIdentity;
using System.Threading.Tasks;

namespace modelo_net_autenticacao.Auth
{
    public static class SefazIdentityProviderBuilder
    {
        public static WsFederationAuthenticationOptions Build(SefazIdentityProvider provider)
        {
            return new WsFederationAuthenticationOptions
            {
                AuthenticationType = provider.Options.UserType,

                // Deve ser igual ao cadastro no Gerenciador de Controle de Acesso do Sefaz Identity.
                Wtrealm = SefazIdentityConfigOptions.Realm,

                // Deve ser um caminho tratado pelo middleware de autenticacao, e nao um controller. Alem disso, funciona com HTTPS, mas nao com HTTP.
                // O middleware vai redirecionar automaticamente ap�s o processamento da resposta da autenticacao.
                Wreply = SefazIdentityConfigOptions.Reply,

                // O componente utilizado pelas aplicacoes clientes depende do valor original do parametro wctx, que e retornado pela v003, mas nao pela v002.
                // Assim, a utilizacao da v003 do Sefaz Identity e obrigatoria para aplicacoes ASP.NET Core 3.1 (utilizam Microsoft.AspNetCore.Authentication.WsFederation).
                MetadataAddress = SefazIdentityConfigOptions.Metadata,
                AuthenticationMode = AuthenticationMode.Passive,
                TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true
                },
                Notifications = new WsFederationAuthenticationNotifications
                {
                    SecurityTokenValidated = provider.OnSecurityTokenValidated,

                    // Necessario para utilizar automaticamente o servixo de validacao de tokens do Sefaz Identity.
                    //SecurityTokenReceived = provider.OnSecurityTokenReceived,

                    // Necessario para poder fornecer os parametros opcionais do Sefaz Identity
                    RedirectToIdentityProvider = arg =>
                    {
                        if (arg.ProtocolMessage.IsSignInMessage)
                        {
                            arg.ProtocolMessage.Wauth = SefazIdentityParams.Wauth;
                            arg.ProtocolMessage.Wfresh = SefazIdentityParams.Wfresh;
                            arg.ProtocolMessage.Parameters.Add("ClaimSets", provider.Options.ClaimSets);
                            arg.ProtocolMessage.Parameters.Add("TipoLogin", provider.Options.TipoLogin);
                            arg.ProtocolMessage.Parameters.Add("AutoLogin", "1");
                            arg.ProtocolMessage.Parameters.Add("Layout", "2");
                        }
                        return Task.FromResult(0);
                    }
                }
            };
        }
    }
}
