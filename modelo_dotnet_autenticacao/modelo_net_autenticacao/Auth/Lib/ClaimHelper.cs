using modelo_net_autenticacao.Auth.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace modelo_net_autenticacao.Auth.Lib
{
    public static class ClaimHelper
    {
        public static System.Security.Claims.Claim GetCurrentUserClaim(string claimType)
        {
            return GetClaimFromPrincipal(Thread.CurrentPrincipal, claimType);
        }

        public static IEnumerable<System.Security.Claims.Claim> GetCurrentUserClaims(string claimType)
        {
            return GetClaimsFromPrincipal(Thread.CurrentPrincipal, claimType);
        }

        public static System.Security.Claims.Claim GetClaimFromPrincipal(IPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                throw new ArgumentException("Cannot convert principal to IClaimsPrincipal.", "principal");
            }
            return GetClaimsFromIdentity(claimsPrincipal.Identities.FirstOrDefault(), claimType).SingleOrDefault();
        }

        private static IEnumerable<System.Security.Claims.Claim> GetClaimsFromPrincipal(IPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                return new List<System.Security.Claims.Claim>();
            }

            return GetClaimsFromIdentity(claimsPrincipal.Identities.FirstOrDefault(), claimType);
        }

        public static IEnumerable<System.Security.Claims.Claim> GetClaimsFromIdentity(IIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                throw new ArgumentException("Cannot convert identity to IClaimsIdentity", "identity");
            }

            return claimsIdentity.Claims.Where(c => c.Type == claimType);
        }

        public static IEnumerable<System.Security.Claims.Claim> ListClaimsFromIdentity(IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                throw new ArgumentException("Cannot convert identity to IClaimsIdentity", "identity");
            }

            return claimsIdentity.Claims.ToList();
        }

        public static void AddClaimToIdentity(string claimType, string value)
        {
            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            ClaimsIdentity claimsIdentity = claimsPrincipal.Identities.FirstOrDefault();

            AddClaimToIdentity(claimsIdentity, claimType, value, String.Empty);
        }

        public static string BuscarValorClaims(string chaveClaim)
        {
            IEnumerable<System.Security.Claims.Claim> claim = GetCurrentUserClaims(chaveClaim);

            if (claim == null)
            {
                return string.Empty;
            }

            if (chaveClaim == SefazIdentityClaimTypes.Nome)
            {
                if (claim.Count() > 0)
                {
                    //Retira CPF/CNPJ do nome (ajuste devido a migração para v002 do Sefaz.Identity)
                    return Regex.Replace(claim.FirstOrDefault().Value, @"[:\d-]", string.Empty).ToUpper();
                }
            }

            return string.Join(",", claim.Select(c => c.Value));
        }

        public static void AddAntiForgeryClaim(ClaimsIdentity identity)
        {
            ClaimHelper.AddClaimToIdentity(
                        identity,
                        AntiForgeryClaimType.NameIdentifier,
                        identity.Name,
                        String.Empty
                        );
        }

        public static void AddClaimToIdentity(ClaimsIdentity identity, string claimType, string claimValue, string issuer)
        {
            var claim = identity.FindFirst(claimType);

            if (claim != null && claim.Type.Equals(claimType))
            {
                identity.RemoveClaim(claim);
            }

            if (String.IsNullOrEmpty(issuer))
            {
                identity.AddClaim(new System.Security.Claims.Claim(
                                            claimType,
                                            claimValue));
            }
            else
            {
                identity.AddClaim(new System.Security.Claims.Claim(
                                            claimType,
                                            claimValue,
                                            null,
                                            issuer));
            }

        }
    }
}
