using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth.Base
{
    public class AntiForgeryClaimType
    {
        /// <summary>
        /// Constante NameIdentifier.
        /// </summary>
        public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        /// <summary>
        /// Constante IdentityProvider.
        /// </summary>
        public const string IdentityProvider = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
    }
}