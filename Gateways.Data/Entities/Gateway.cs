
namespace Gateways.Data.Entities
{
    public class Gateway
    {
        public int Id { get; set; }
        public Guid SerialNumber { get; set; }
        public string name { get; set; }
        public string IpV4 { get; set; }
        public ICollection<Peripheral> Peripherals { get; set; }
    }
}
