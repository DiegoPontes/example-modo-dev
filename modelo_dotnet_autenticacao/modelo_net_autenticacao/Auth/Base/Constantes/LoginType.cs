using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth.Base
{
    public static class LoginType
    {
        public static string GOV_BR => "GOV.BR";
        public static string IDP_SP => "IDP.SP";
        public static string SEFAZ_IDENTITY => "SEFAZ_ID";
    }
}