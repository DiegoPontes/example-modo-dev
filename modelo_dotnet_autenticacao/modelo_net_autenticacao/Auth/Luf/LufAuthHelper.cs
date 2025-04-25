using Microsoft.Owin.Security;
using modelo_net_autenticacao.Auth.Base;
using modelo_net_autenticacao.Auth.Base.Selo;
using modelo_net_autenticacao.Auth.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Web;

namespace modelo_net_autenticacao.Auth.Luf
{
    public static class LufAuthHelper
    {
        public static bool EstaAutenticadoComTipoLoginPermitido()
        {
            string[] tiposDeLoginPermitidos = { "x509" }; //Tipos suportados: passwd, bank, x509

            var amr = ClaimHelper.BuscarValorClaims(LufClaimTypes.AMR);

            return (tiposDeLoginPermitidos.Any(t => t == amr));
        }

        public static bool EstaAutenticadoNoLoginUnicoFederal()
        {
            var amr = ClaimHelper.BuscarValorClaims(LufClaimTypes.AMR);

            return (!String.IsNullOrWhiteSpace(amr));
        }

        public static void LoginUnicoGovBr(HttpRequestBase request)
        {
            var opts = new OidcConfigOptions(AuthProviderPrefix.LUF);

            if (!request.IsAuthenticated)
            {
                request.RequestContext.HttpContext.Session.Clear();
                request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = opts.RedirectUri },
                    opts.Type);
            }
        }

        public static void AdicionarSelo(ClaimsIdentity identity, string niveis)
        {
            String valorSelo = NivelAutenticacao.Nenhum.ToString();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                List<ClaimNivelAutenticacao> selos = JsonSerializer.Deserialize<List<ClaimNivelAutenticacao>>(niveis, options);

                if (selos != null && selos.Count > 0)
                {
                    int[] valoresValidos = { 1, 2, 3 };
                    var selo = selos.OrderByDescending(x => x.Ordem(valoresValidos)).First();
                    valorSelo = NivelAutenticacaoExtensions.ToEnum(selo.Id).ToString();
                }
            }
            catch
            {
                valorSelo = NivelAutenticacao.Nenhum.ToString();

            }

            ClaimHelper.AddClaimToIdentity(
                identity, AppClaims.SeloType,
                valorSelo,
                AppConfigOptions.AppClaimsKey);
        }
    }
}