namespace modelo_net_autenticacao.Auth
{
    public static class ClaimTypes
    {
        /// <summary>
        /// No caso do LUF, este campo traz o CPF (Certificado Digital)
        /// </summary>
        public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        /// <summary>
        /// No caso do LUF, este campo traz o nome do usuário (Certificado Digital)
        /// </summary>
        public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    }
}
