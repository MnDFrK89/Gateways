using AutoMapper;
using Gateways.Data.Entities;
using Gateways.Data.DTO.Response;

namespace Gateways.API.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Gateway, GatewayResponseDTO>()
                .ForMember(g => g.SerialNumber, opts => opts.MapFrom(source => source.SerialNumber))
                .ForMember(g => g.IpV4, opts => opts.MapFrom(source => source.IpV4))
                .ForMember(g => g.name, opts => opts.MapFrom(source => source.name))
                .ForMember(g => g.Peripherals, opts => opts.MapFrom(source => source.Peripherals));

            CreateMap<Peripheral, PeripheralResponseDTO>()
                .ForMember(g => g.Status, opts => opts.MapFrom(source => source.Status))
                .ForMember(g => g.Created, opts => opts.MapFrom(source => source.Created))
                .ForMember(g => g.Vendor, opts => opts.MapFrom(source => source.Vendor))
                .ForMember(g => g.UID, opts => opts.MapFrom(source => source.UID));
        }
    }
}