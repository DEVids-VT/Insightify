using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Fetching.Exceptions
{
    public class FetchDataException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public FetchDataException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public FetchDataException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
