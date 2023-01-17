using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Exceptions
{
    public class BaseNotFoundException : CustomBaseException
    {
        public BaseNotFoundException() : base()
        {
            HttpCode = (int)HttpStatusCode.NotFound;
        }
    }
}
