using Gateways.Data.DTO.Request;
using Gateways.Data.UoW;
using Gateways.Data.Entities;
using Gateways.Services.Exceptions.NotFound;
using Microsoft.Extensions.Localization;
using Gateways.Services.Exceptions.BadRequest;

namespace Gateways.Services.Impls
{

    public class PeripheralService : IPeripheralService
    {
        private readonly IUnitOfWork _uow;
        private readonly IStringLocalizer<IGatewayService> _localizer;
        private readonly IGatewayService _gateway;
        public PeripheralService(IUnitOfWork uow, IStringLocalizer<IGatewayService> localizer, IGatewayService gateway)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _gateway = gateway ?? throw new ArgumentNullException(nameof(gateway));
        }
        public async Task<Peripheral> AddPeripheralAsync(PeripheralRequestDTO request, Guid gatewayUID)
        {
            var gateway = _gateway.GetGateway(gatewayUID) ?? throw new GatewayNotFoundException(_localizer);

            if (gateway.Peripherals.Count == 10) throw new FullPeripheralsBadRequestException(_localizer);

            var response = new Peripheral
            {
                Created = request.Created,
                GatewayId = gateway.Id,
                Status = request.Status,
                UID = Guid.NewGuid(),
                Vendor = request.Vendor
            };
            await _uow.PeripheralsRepository.AddAsync(response);
            await _uow.CommitAsync();

            return response;
        }

        public async Task RemovePeripheralAsync(Guid peripheralUid)
        {
            var peripheral =  await _uow.PeripheralsRepository.FirstOrDefaultAsync(p => p.UID == peripheralUid) ?? throw new PeripheralNotFoundException(_localizer);
            _uow.PeripheralsRepository.Delete(peripheral);

            await _uow.CommitAsync();
        }
    }
}
