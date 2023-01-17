using Microsoft.Extensions.Localization;
using System.Text;

namespace Gateways.Services.Exceptions.BadRequest
{
    public class FullPeripheralsBadRequestException:BaseBadRequestException
    {
        public FullPeripheralsBadRequestException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 400001;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}
