using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace modelo_net_autenticacao.Auth.Base.Selo
{
    public class ClaimNivelAutenticacao
    {
        public String Id { get; set; }
        public String DataAtualizacao { get; set; }

        public int Ordem(int[] dominio = null)
        {
            if (int.TryParse(Id, out int ordem))
            {
                if (dominio != null && !dominio.Contains(ordem))
                {
                    return Int32.MinValue;
                }

                return ordem;
            }

            return Int32.MinValue;
        }

        public static ClaimNivelAutenticacao RetornarMaiorOrdem(String json)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<ClaimNivelAutenticacao> selos = JsonSerializer.Deserialize<List<ClaimNivelAutenticacao>>(json, options);

                if (selos != null && selos.Count > 0)
                {
                    return selos.OrderByDescending(x => x.Ordem()).First();
                }
            }
            catch
            { }

            return null;
        }
    }
}