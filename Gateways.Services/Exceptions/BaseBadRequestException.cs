using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Exceptions
{
    public class BaseBadRequestException : CustomBaseException
    {
        public BaseBadRequestException() : base()
        {
            HttpCode = (int)HttpStatusCode.BadRequest;
        }
        public BaseBadRequestException(string message) : base(message)
        {
            HttpCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
