using Gateways.Data.Entities;
using Gateways.Data.DTO.Request;
using Gateways.Data.DTO.Response;

namespace Gateways.Services
{
    public interface IGatewayService
    {
        ICollection<Gateway> GetAll();
        Task AddGatewayAsync(GatewayRequestDTO request);
        Gateway GetGateway(Guid gatewayUid);
    }
}
