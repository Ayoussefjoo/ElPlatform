using ElPlatform.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.Client.Services.Exceptions
{
    public class ApiException :Exception
    {
        public ApiErrorResponse ApiErrorResponse { get; set; }
        public HttpStatusCode httpStatusCode { get; set; }
        public ApiException(ApiErrorResponse erorr, HttpStatusCode statusCode):this(erorr)
        {
            httpStatusCode = statusCode;
        }
        public ApiException(ApiErrorResponse erorr)
        {
            ApiErrorResponse = erorr;
        }
    }
}
