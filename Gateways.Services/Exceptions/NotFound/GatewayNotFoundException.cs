using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Exceptions.NotFound
{
    public class GatewayNotFoundException:BaseNotFoundException
    {
        public GatewayNotFoundException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 404001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}
