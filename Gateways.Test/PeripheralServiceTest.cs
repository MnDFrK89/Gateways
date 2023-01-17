using Gateways.Data;
using Gateways.Data.Entities;
using Gateways.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using Gateways.Data.Enums;
using Xunit;
using Gateways.Services.Impls;
using Gateways.Data.UoW;
using Gateways.Data.DTO.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gateways.Test
{
    public abstract class PeripheralServiceTest
    {

        protected DbContextOptions<CoreDbContext> ContextOptions { get; }

        protected PeripheralServiceTest(DbContextOptions<CoreDbContext> contextOptions)
        {
            ContextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));

            _localizerMock = new Mock<IStringLocalizer<IGatewayService>>().Object;

            SeedAsync()
               .Wait();
        }

        private readonly IStringLocalizer<IGatewayService> _localizerMock;
        private async Task SeedAsync()
        {
            await using var context = new CoreDbContext(ContextOptions);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await context.Peripherals.AddRangeAsync(new Peripheral
            {
                Id = 1,
                Created = DateTime.Now,
                GatewayId = 1,
                Status = PeripheralStatusEnum.OFFLINE,
                UID = Guid.Parse("860d417d-9255-4ba5-876b-238f21b1f3d1"),
                Vendor = "Intel"


            }, new Peripheral
            {
                Id = 2,
                Created = DateTime.Now,
                GatewayId = 1,
                Status = PeripheralStatusEnum.ONLINE,
                UID = Guid.Parse("7dbd7c75-f43a-4123-991b-2b23d7be4f91"),
                Vendor = "Intel"
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddPeripheral()
        {
            // Arrange

            var gatewayGuid = Guid.Parse("43e3f109-5e61-47e0-bd84-4e75dc64ccb8");
            var gatewayId = 1;

            var fakePeripheralRequest = new PeripheralRequestDTO
            {
                Created = DateTime.Now,
                Status = PeripheralStatusEnum.ONLINE,
                Vendor = "Asus"
            };
            await using var context = new CoreDbContext(ContextOptions);
            var uow = new UnitOfWork(context);
            var gatewayService = new GatewayService(uow, _localizerMock);
            var service = new PeripheralService(uow, _localizerMock, gatewayService);

            // Act
            await service.AddPeripheralAsync(fakePeripheralRequest, gatewayGuid);

            // Assert
            var created = await context.Peripherals.FirstOrDefaultAsync(g => g.GatewayId == gatewayId);
            Assert.Equals(created?.GatewayId, 1);
        }

        [Fact]
        public async Task RemovePeripheral()
        {
            // Arrange
            Guid peripheralId = Guid.Parse("860d417d-9255-4ba5-876b-238f21b1f3d1");

            await using var context = new CoreDbContext(ContextOptions);
            var uow = new UnitOfWork(context);
            var gatewayService = new GatewayService(uow, _localizerMock);
            var service = new PeripheralService(uow, _localizerMock, gatewayService);

            // Act

            await service.RemovePeripheralAsync(peripheralId);
            Assert.Equals(Guid.Parse("7dbd7c75-f43a-4123-991b-2b23d7be4f91"), context.Peripherals.FirstOrDefault()?.UID);
        }
    }
}
