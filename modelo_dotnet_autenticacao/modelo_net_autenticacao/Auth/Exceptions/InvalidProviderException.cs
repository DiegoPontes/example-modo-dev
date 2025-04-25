using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace modelo_net_autenticacao.Auth.Exceptions
{
    public class InvalidProviderException : ApplicationException
    {
        public InvalidProviderException()
        {
        }

        public InvalidProviderException(string message) : base(message)
        {
        }

        public InvalidProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}