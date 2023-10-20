using AutoMapper;
using Engie.Contracts.Dto;
using Engie.Domain.Models;

namespace Engie.Business.mappers
{
    public class ProductionPlanMapper : Profile
    {
        public ProductionPlanMapper()
        {
            CreateMap<FuelsDto, Fuels>()
               .ReverseMap();
            CreateMap<PayloadDto, Payload>()
               .ReverseMap();
            CreateMap<PowerplantDto, Powerplant>()
               .ReverseMap();
            CreateMap<ResponseDto, Response>()
               .ReverseMap();
        }
    }
}
