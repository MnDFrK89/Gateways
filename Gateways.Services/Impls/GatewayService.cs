using Gateways.Data.DTO.Request;
using Gateways.Data.Entities;
using Gateways.Data.UoW;
using Gateways.Services.Exceptions.NotFound;
using Microsoft.Extensions.Localization;

namespace Gateways.Services.Impls
{
    public class GatewayService : IGatewayService
    {
        private readonly IUnitOfWork _uow;
        private readonly IStringLocalizer<IGatewayService> _localizer;

        public GatewayService(IUnitOfWork uow, IStringLocalizer<IGatewayService> localizer)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public async Task AddGatewayAsync(GatewayRequestDTO request)
        {
            await _uow.GatewaysRepository.AddAsync(new Gateway
            {
                IpV4 = request.IpV4,
                name = request.name,
                SerialNumber = Guid.NewGuid()
            });

            await _uow.CommitAsync();
        }

        public ICollection<Gateway> GetAll()
        {
            return _uow.GatewaysRepository.GetAllIncluding(c => c.Peripherals).ToList();
        }

        public Gateway GetGateway(Guid gatewayUid)
        {
            return _uow.GatewaysRepository.GetAllIncluding(g => g.Peripherals).FirstOrDefault(g => g.SerialNumber == gatewayUid) ?? throw new GatewayNotFoundException(_localizer);
        }
    }
}
