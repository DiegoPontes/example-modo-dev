using Microsoft.IdentityModel.Protocols.WsFederation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.WsFederation;
using modelo_net_autenticacao.Auth.Base;
using modelo_net_autenticacao.Auth.Lib;
using modelo_net_autenticacao.Auth.Luf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace modelo_net_autenticacao.Auth.SefazIdentity
{
    public class SefazIdentityProvider
    {
        public readonly SefazIdentityConfigOptions Options;
        public SefazIdentityProvider(SefazIdentityConfigOptions options)
        {
            Options = options;
        }

        public Task OnSecurityTokenValidated(SecurityTokenValidatedNotification<WsFederationMessage, WsFederationAuthenticationOptions> arg)
        {
            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                AppClaims.LoginType,
                LoginType.SEFAZ_IDENTITY,
                AppClaims.Issuer
                );

            ClaimHelper.AddClaimToIdentity(
                arg.AuthenticationTicket.Identity,
                AppClaims.SeloType,
                NivelAutenticacao.CertificadoDigital.ToString(),
                AppConfigOptions.AppClaimsKey);

            ClaimHelper.AddAntiForgeryClaim(arg.AuthenticationTicket.Identity);

            IncluirClaimTipoUsuario(arg);

            return Task.FromResult(0);
        }

        public Task OnSecurityTokenReceived(SecurityTokenReceivedNotification<WsFederationMessage, WsFederationAuthenticationOptions> notification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Regra de negócio para definir o tipo de usuário. Essa operação é necessária pois o Identity não volta
        /// o authentication type. Com isso, o OWIN executa o callback SecurityTokenValidated de todas as configurações
        /// do WSFederation.
        /// </summary>
        /// <param name="arg">Response da autenticação via OWIN com o SEFAZ Identity</param>
        private void IncluirClaimTipoUsuario(SecurityTokenValidatedNotification<WsFederationMessage, WsFederationAuthenticationOptions> arg)
        {
            var claims = arg.AuthenticationTicket.Identity.Claims.ToList();

            if (claims != null)
            {
                var siap = claims.FirstOrDefault(c => c.Type.Equals(SefazIdentityClaimTypes.SIAP));

                if (siap != null && !String.IsNullOrWhiteSpace(siap.Value))
                {
                    ClaimHelper.AddClaimToIdentity(
                        arg.AuthenticationTicket.Identity,
                        AppClaims.UserType,
                        TipoUsuarioAutenticado.FAZENDARIO,
                        AppClaims.Issuer);
                }
                else
                {
                    var cnpj = claims.FirstOrDefault(c => c.Type.Equals(SefazIdentityClaimTypes.CNPJ));

                    if (cnpj != null && !String.IsNullOrWhiteSpace(cnpj.Value))
                    {
                        ClaimHelper.AddClaimToIdentity(
                            arg.AuthenticationTicket.Identity,
                            AppClaims.UserType,
                            TipoUsuarioAutenticado.PESSOA_JURIDICA,
                            AppClaims.Issuer);
                    }
                    else
                    {
                        ClaimHelper.AddClaimToIdentity(
                            arg.AuthenticationTicket.Identity,
                            AppClaims.UserType,
                            TipoUsuarioAutenticado.PESSOA_FISICA,
                            AppClaims.Issuer);
                    }
                }
            }
        }
    }
}