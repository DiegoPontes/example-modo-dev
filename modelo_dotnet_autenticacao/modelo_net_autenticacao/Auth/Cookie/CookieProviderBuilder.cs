using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Owin;
using System;
using System.Globalization;
using Microsoft.Owin.Security;

namespace modelo_net_autenticacao.Auth
{
    public static class CookieProviderBuilder
    {
        public static void Build(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType, //DefaultAuthenticationTypes.ExternalCookie, //
                CookieName = AppConfigOptions.CookieName,
                CookieHttpOnly = true,
                CookieSameSite = Microsoft.Owin.SameSiteMode.Lax,
                CookieSecure = CookieSecureOption.Always,
                ExpireTimeSpan = new TimeSpan(0, 0, 60, 0),
                SlidingExpiration = false,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                CookiePath = AppConfigOptions.CookiePath,
                CookieManager = new SystemWebChunkingCookieManager()
            });

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }        
    }
}
