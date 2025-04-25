using Microsoft.IdentityModel.Logging;
using Microsoft.Owin;
using modelo_net_autenticacao.Auth;
using modelo_net_autenticacao.Auth.Base;
using modelo_net_autenticacao.Auth.SefazIdentity;
using Owin;

[assembly: OwinStartup(typeof(modelo_net_autenticacao.Startup))]

namespace modelo_net_autenticacao
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            IdentityModelEventSource.ShowPII = true;

            CookieProviderBuilder.Build(app);

            // Ativa o Login Unico Federal
            app.UseOpenIdConnectAuthentication(OidcProviderBuilder.Build(new LufProvider()));

            //// Ativa o IDP SP
            app.UseOpenIdConnectAuthentication(OidcProviderBuilder.Build(new IdpSpProvider()));

            //// Ativa o Sefaz Identity para Fazendarios
            app.UseWsFederationAuthentication(SefazIdentityProviderBuilder.Build(
                new SefazIdentityProvider
                (
                    new SefazIdentityConfigOptions(
                    TipoUsuarioAutenticado.FAZENDARIO,
                    SefazIdentityParams.ClaimSetsFazendario,
                    SefazIdentityParams.TipoLoginFazendario))));

            //// Ativa o Sefaz Identity para Pessoa Fisica
            //app.UseWsFederationAuthentication(SefazIdentityProviderBuilder.Build(
            //    new SefazIdentityProvider
            //    (
            //        new SefazIdentityConfigOptions(
            //        TipoUsuarioAutenticado.PESSOA_FISICA,
            //        SefazIdentityParams.ClaimSetsContribuinte,
            //        SefazIdentityParams.TipoLoginContribuintePF))));

            //// Ativa o Sefaz Identity para Pessoa Juridica
            //app.UseWsFederationAuthentication(SefazIdentityProviderBuilder.Build(
            //    new SefazIdentityProvider
            //    (
            //        new SefazIdentityConfigOptions(
            //        TipoUsuarioAutenticado.PESSOA_JURIDICA,
            //        SefazIdentityParams.ClaimSetsContribuinte,
            //        SefazIdentityParams.TipoLoginContribuintePJ))));

        }
    }
}