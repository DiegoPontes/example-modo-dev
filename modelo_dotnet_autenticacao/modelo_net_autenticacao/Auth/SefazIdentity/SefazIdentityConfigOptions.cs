using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth.SefazIdentity
{
    public class SefazIdentityConfigOptions
    {
        public string UserType { get; internal set; }

        public string ClaimSets { get; internal set; }

        public string TipoLogin { get; internal set; }


        public SefazIdentityConfigOptions(string userType, string claimSets, string tipoLogin)
        { 
            UserType = userType;
            ClaimSets = claimSets;
            TipoLogin = tipoLogin;
        }

        public static string Realm => ConfigurationManager.AppSettings["si:Realm"];
        public static string Reply => ConfigurationManager.AppSettings["si:Reply"];
        public static string Metadata => ConfigurationManager.AppSettings["si:Metadata"];        
    }
}