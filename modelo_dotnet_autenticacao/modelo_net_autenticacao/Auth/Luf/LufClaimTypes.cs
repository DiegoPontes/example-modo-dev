namespace modelo_net_autenticacao.Auth
{
    public static class LufClaimTypes
    {
        /// <summary>
        /// CPF do usuário autenticado.
        /// </summary>
        public const string SUB = "sub";

        /// <summary>
        /// Client ID da aplicação onde o usuário se autenticou.
        /// </summary>
        public const string AUD = "aud";

        /// <summary>
        /// Escopos autorizados pelo provedor de autenticação.
        /// </summary>
        public const string SCOPE = "scope";

        /// <summary>
        /// Listagem dos fatores de autenticação do usuário. 
        /// Pode ser: 
        /// - "passwd" se o mesmo logou fornecendo a senha, 
        /// - "x509" se o mesmo utilizou certificado digital ou certificado em nuvem, ou 
        /// - "bank" para indicar utilização de conta bancária para autenticar. 
        /// Esse último seguirá com número de identificação do banco, 
        /// conforme código de compensação do Bacen presente ao final da explicação.
        /// </summary>
        public const string AMR = "amr";

        /// <summary>
        /// URL do provedor de autenticação que emitiu o token.
        /// </summary>
        public const string ISS = "iss";

        /// <summary>
        /// Data/hora de expiração do token.
        /// </summary>
        public const string EXP = "exp";

        /// <summary>
        /// Data/hora em que o token foi emitido.
        /// </summary>
        public const string IAT = "iat";

        /// <summary>
        /// Identificador único do token, reconhecido internamente pelo provedor de autenticação.
        /// </summary>
        public const string JTI = "jti";

        /// <summary>
        /// CNPJ vinculado ao usuário autenticado. Atributo será preenchido quando autenticação ocorrer 
        /// por certificado digital de pessoal jurídica.
        /// </summary>
        public const string CNPJ = "cnpj";

        /// <summary>
        /// NOME do usuário autenticado.
        /// </summary>
        public const string NAME = "name";
    }
}
