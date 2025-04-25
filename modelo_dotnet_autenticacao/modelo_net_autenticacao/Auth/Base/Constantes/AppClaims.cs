using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth.Base
{
    public static class AppClaims
    {
        /// <summary>
        /// Issuer.
        /// </summary>
        public const string Issuer = "http://schemas.fazenda.sp.gov.br/app/issuer";

        // <summary>
        /// User type.
        /// </summary>
        public const string UserType = "http://schemas.fazenda.sp.gov.br/app/claims/user-type";

        /// <summary>
        /// Login type.
        /// </summary>
        public const string LoginType = "http://schemas.fazenda.sp.gov.br/app/claims/login-type";

        /// <summary>
        /// Selo type.
        /// </summary>
        public const string SeloType = "http://schemas.fazenda.sp.gov.br/app/claims/selo";
    }
}