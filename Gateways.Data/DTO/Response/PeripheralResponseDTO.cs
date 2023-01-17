using Gateways.Data.Enums;


namespace Gateways.Data.DTO.Response
{
    public class PeripheralResponseDTO
    {
        public Guid UID { get; set; }
        public string Vendor { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}
