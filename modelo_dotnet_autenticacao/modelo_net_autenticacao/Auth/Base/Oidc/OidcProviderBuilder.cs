using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Net;
using System.Threading.Tasks;

namespace modelo_net_autenticacao.Auth
{
    public static class OidcProviderBuilder
    {
        public static OpenIdConnectAuthenticationOptions Build(OidcProvider provider)
        {
            if (provider == null || provider.Options == null)
                throw new System.ArgumentNullException("Configurações do provedor OIDC não encontradas.");

            return new OpenIdConnectAuthenticationOptions
            {
                ClientId = provider.Options.ClientId,
                ClientSecret = provider.Options.ClientSecret,
                AuthenticationType = provider.Options.Type,
                AuthenticationMode = AuthenticationMode.Passive,
                Authority = provider.Options.Authority,
                Scope = provider.Options.Scopes,
                RedirectUri = provider.Options.RedirectUri,
                PostLogoutRedirectUri = provider.Options.LogoutUri,
                RequireHttpsMetadata = true,
                ResponseType = OpenIdConnectResponseType.Code,
                RedeemCode = true,
                /*ProtocolValidator = new OpenIdConnectProtocolValidator
                {
                    RequireStateValidation = false,
                    RequireNonce = false,
                    NonceLifetime = TimeSpan.FromMinutes(15)
                },*/
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true
                },
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = provider.OnSecurityTokenValidated,
                    AuthenticationFailed = provider.OnAuthenticationFailed,
                    AuthorizationCodeReceived = provider.OnAuthorizationCodeReceived,
                    MessageReceived = provider.OnMessageReceived,
                    RedirectToIdentityProvider = provider.OnRedirectToIdentityProvider,
                    SecurityTokenReceived = provider.OnSecurityTokenReceived,
                    TokenResponseReceived = provider.OnTokenResponseReceived
                }
            };
        }
    }
}
