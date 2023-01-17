using Gateways.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Gateways.Data.DTO.Request
{
    public class GatewayRequestDTO
    {
        public string name { get; set; }
        [Required]
        public string IpV4 { get; set; }
    }
}
