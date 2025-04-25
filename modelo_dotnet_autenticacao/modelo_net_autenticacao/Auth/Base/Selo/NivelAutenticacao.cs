using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace modelo_net_autenticacao.Auth.Luf
{
    [Serializable]
    public enum NivelAutenticacao : short
    {
        Nenhum = 0,
        Bronze = 1,
        Prata = 2,
        Ouro = 3,
        CertificadoDigital = 6
    }

    public static class NivelAutenticacaoExtensions
    {
        public static string Descricao(this NivelAutenticacao nivelAutenticacao)
        {
            switch (nivelAutenticacao)
            {
                case NivelAutenticacao.Nenhum: return "Nenhum";
                case NivelAutenticacao.Bronze: return "Bronze";
                case NivelAutenticacao.Ouro: return "Ouro";
                case NivelAutenticacao.CertificadoDigital: return "Certificado digital";
                default: return nivelAutenticacao.ToString();
            }
        }

        public static NivelAutenticacao ToEnum(String id)
        {
            switch (id)
            {
                case "1": return NivelAutenticacao.Bronze;
                case "2": return NivelAutenticacao.Prata;
                case "3": return NivelAutenticacao.Ouro;
                case "6": return NivelAutenticacao.CertificadoDigital;
                default: return NivelAutenticacao.Nenhum;
            }
        }

    }
}