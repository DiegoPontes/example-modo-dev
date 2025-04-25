using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth.SefazIdentity
{
    public class SefazIdentityParams
    {
        public static string ClaimSetsFazendario => "80F30E33";

        public static string TipoLoginFazendario => "00000001";


        public static string ClaimSetsContribuinte => "80000000";

        public static string TipoLoginContribuintePJ => "00000002";

        public static string TipoLoginContribuintePF => "00000001";

        public static string Wauth => "urn:oasis:names:tc:SAML:1.0:assertion";

        public static string Wfresh => "60";
    }
}