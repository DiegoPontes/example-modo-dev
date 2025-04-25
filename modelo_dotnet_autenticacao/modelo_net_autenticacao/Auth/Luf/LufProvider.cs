using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using modelo_net_autenticacao.Auth.Base;
using modelo_net_autenticacao.Auth.Lib;
using modelo_net_autenticacao.Auth.Luf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace modelo_net_autenticacao.Auth
{
    public class LufProvider : OidcProvider
    {
        public LufProvider() : base(AuthProviderPrefix.LUF) { }
        

        public override Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg)
        {
            // If signing out, add the id_token_hint
            if (arg.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
            {
                var idTokenHint = arg.OwinContext.Authentication.User.FindFirst("id_token");

                if (idTokenHint != null)
                {
                    arg.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                }
            }
            return Task.FromResult(0);
        }

        public override async Task OnSecurityTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg)
        {
            string cpf = arg.AuthenticationTicket.Identity.FindFirst(LufClaimTypes.SUB).Value;
            var nome = arg.AuthenticationTicket.Identity.FindFirst(LufClaimTypes.NAME).Value;

            // Obtém os níveis do usuário que se autenticou no Login Único Federal
            string niveis = await OidcUtils.GetString($"{Options.ApiUrl}/confiabilidades/v3/contas/{cpf}/niveis?response-type=ids", AppConfigOptions.Proxy, "cs_git_servico", "git_cia", arg.ProtocolMessage.AccessToken);
            if (!string.IsNullOrEmpty(niveis))
            {
                ClaimHelper.AddClaimToIdentity(arg.AuthenticationTicket.Identity, "niveis", niveis, Options.ClaimsKey);
                LufAuthHelper.AdicionarSelo(arg.AuthenticationTicket.Identity, niveis);
            }

            // Obtém as categorias do usuário que se autenticou no Login Único Federal
            string categorias = await OidcUtils.GetString($"{Options.ApiUrl}/confiabilidades/v3/contas/{cpf}/categorias?response-type=ids", AppConfigOptions.Proxy, "cs_git_servico", "git_cia", arg.ProtocolMessage.AccessToken);
            if (!string.IsNullOrEmpty(categorias))
            {
                ClaimHelper.AddClaimToIdentity(arg.AuthenticationTicket.Identity, "categorias", categorias, Options.ClaimsKey);
            }

            // Obtém as confiabilidades do usuário que se autenticou no Login Único Federal
            string confiabilidades = await OidcUtils.GetString($"{Options.ApiUrl}/confiabilidades/v3/contas/{cpf}/confiabilidades?response-type=ids", AppConfigOptions.Proxy, "cs_git_servico", "git_cia", arg.ProtocolMessage.AccessToken);
            if (!string.IsNullOrEmpty(confiabilidades))
            {
                ClaimHelper.AddClaimToIdentity(arg.AuthenticationTicket.Identity, "confiabilidades", confiabilidades, Options.ClaimsKey);
            }

            // Obteria (se funcionasse) as empresas do usuário que se autenticou no Login Único Federal
            string empresas = await OidcUtils.GetString($"{Options.ApiUrl}/empresas/v2/empresas?filtrar-por-participante={cpf}", AppConfigOptions.Proxy, "cs_git_servico", "git_cia", arg.ProtocolMessage.AccessToken);
            if (!string.IsNullOrEmpty(empresas))
            {
                ClaimHelper.AddClaimToIdentity(arg.AuthenticationTicket.Identity, "empresas", empresas, Options.ClaimsKey);
            }

            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                AppClaims.LoginType,
                LoginType.GOV_BR,
                AppClaims.Issuer);

            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                AppClaims.UserType,
                TipoUsuarioAutenticado.PESSOA_FISICA,
                AppClaims.Issuer);

            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                AntiForgeryClaimType.NameIdentifier,
                $"{nome}:{cpf}",
                AppClaims.Issuer
            );

            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                SefazIdentityClaimTypes.CPF,
                cpf,
                AppClaims.Issuer
            );

            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                SefazIdentityClaimTypes.Nome,
                nome,
                AppClaims.Issuer
            );

            await Task.FromResult(0);
        }        
    }
}