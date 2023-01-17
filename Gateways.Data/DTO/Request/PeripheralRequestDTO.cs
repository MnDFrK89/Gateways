using Gateways.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Data.DTO.Request
{
    public class PeripheralRequestDTO
    {
        public string Vendor { get; set; }
        public DateTime Created { get; set; }
        public PeripheralStatusEnum Status { get; set; }
    }
}
