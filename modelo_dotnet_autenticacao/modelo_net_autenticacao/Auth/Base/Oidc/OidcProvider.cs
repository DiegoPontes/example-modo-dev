using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo_net_autenticacao.Auth
{
    public abstract class OidcProvider
    {
        public OidcConfigOptions Options { get; set; }

        public OidcProvider(string prefix)
        {
            Options = new OidcConfigOptions(prefix);
        }


        public virtual Task OnTokenResponseReceived(TokenResponseReceivedNotification arg) => Task.FromResult(0);

        public virtual Task OnSecurityTokenReceived(SecurityTokenReceivedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg) => Task.FromResult(0);

        public virtual Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg) => Task.FromResult(0);

        public virtual Task OnMessageReceived(MessageReceivedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg) => Task.FromResult(0);

        public virtual Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification arg) => Task.FromResult(0);

        public virtual Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg)=> Task.FromResult(0);

        public virtual Task OnSecurityTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg)=> Task.FromResult(0);
    }
}
