using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth
{
    public static class AppConfigOptions
    {
        public static string AppClaimsKey => ConfigurationManager.AppSettings["app:ClaimsKey"];
        public static string CookieName => ConfigurationManager.AppSettings["app:CookieName"];
        public static string CookiePath => ConfigurationManager.AppSettings["app:CookiePath"];
        public static string Proxy => ConfigurationManager.AppSettings["app:Proxy"];


    }
}