using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Domain.Common.Exceptions
{

    // this class any exception in my app will inhirte from it
    public class ApplicationExceptionSystem : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorDetails { get; }
        public string ResourceName { get; }

        public ApplicationExceptionSystem(string resourceName, string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message)

        {
            ResourceName = resourceName;
            StatusCode = statusCode;
            ErrorDetails = message;

        }
    }
}
