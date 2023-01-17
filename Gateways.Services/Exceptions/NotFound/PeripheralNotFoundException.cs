using Microsoft.Extensions.Localization;
using System.Text;


namespace Gateways.Services.Exceptions.NotFound
{
    public class PeripheralNotFoundException:BaseNotFoundException
    {
        public PeripheralNotFoundException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 404002;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}
