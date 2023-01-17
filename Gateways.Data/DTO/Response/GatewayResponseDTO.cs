using Gateways.Data.Entities;

namespace Gateways.Data.DTO.Response
{
    public class GatewayResponseDTO
    {
        public Guid SerialNumber { get; set; }
        public string name { get; set; }
        public string IpV4 { get; set; }
        public ICollection<PeripheralResponseDTO> Peripherals { get; set; }
    }
}
