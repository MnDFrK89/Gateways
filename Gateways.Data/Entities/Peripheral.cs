using Gateways.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Data.Entities
{
    public class Peripheral
    {
        public int Id { get; set; }
        public Guid UID { get; set; }
        public string Vendor { get; set; }
        public PeripheralStatusEnum Status { get; set; }
        public DateTime Created { get; set; }
        public int GatewayId { get; set; }
        public Gateway Gateway { get; set; }
    }
}
