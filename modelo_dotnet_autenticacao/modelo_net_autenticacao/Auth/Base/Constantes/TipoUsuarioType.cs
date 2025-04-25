using Microsoft.Ajax.Utilities;
using System;

namespace modelo_net_autenticacao.Auth.Base
{
    [Serializable]
    public static class TipoUsuarioAutenticado
    {
        public const string PESSOA_FISICA = "PESSOA_FISICA";
        public const string PESSOA_JURIDICA = "PESSOA_JURIDICA";
        public const string FAZENDARIO = "FAZENDARIO";

    }
}