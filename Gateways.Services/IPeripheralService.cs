using Gateways.Data.DTO.Request;
using Gateways.Data.Entities;

namespace Gateways.Services
{
    public interface IPeripheralService
    {
        Task<Peripheral> AddPeripheralAsync(PeripheralRequestDTO request, Guid gatewayUID);
        Task RemovePeripheralAsync(Guid peripheralUID);
    }
}
