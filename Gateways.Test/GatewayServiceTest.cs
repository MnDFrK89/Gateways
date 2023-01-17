using Gateways.Data;
using Gateways.Data.DTO.Request;
using Gateways.Data.Entities;
using Gateways.Data.UoW;
using Gateways.Services;
using Gateways.Services.Impls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gateways.Test
{
    public abstract class GatewayServiceTest
    {

        protected DbContextOptions<CoreDbContext> ContextOptions { get; }

        protected GatewayServiceTest(DbContextOptions<CoreDbContext> contextOptions)
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

            await context.Gateways.AddRangeAsync(new Gateway
            {
                IpV4 = "10.0.0.1",
                name = "Asus",
                SerialNumber = Guid.Parse("406043be-77e2-4261-9dad-59e240737d45")

            }, new Gateway
            {
                IpV4 = "10.0.0.2",
                name = "Intel",
                SerialNumber = Guid.Parse("98a3cb91-e549-4ab5-8db6-433c23d86b67")
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllGateways()
        {

            await using var context = new CoreDbContext(ContextOptions);
            var service = new GatewayService(new UnitOfWork(context), _localizerMock);

            // Act
            var getAll = service.GetAll();

            // Assert
            Assert.Equals(Guid.Parse("406043be-77e2-4261-9dad-59e240737d45"), getAll.FirstOrDefault()?.SerialNumber);
        }

        [Fact]
        public async Task AddGateway()
        {
            // Arrange

            var fakeGatewayRequest = new GatewayRequestDTO
            {
                IpV4 = "10.0.0.2",
                name = "Intel",
            };
            await using var context = new CoreDbContext(ContextOptions);
            var service = new GatewayService(new UnitOfWork(context), _localizerMock);

            // Act
            await service.AddGatewayAsync(fakeGatewayRequest);

            // Assert
            var created = await context.Gateways.FirstOrDefaultAsync(g => g.Id == 3);
            Assert.Equals(created?.Id, 3);
        }
    }
}