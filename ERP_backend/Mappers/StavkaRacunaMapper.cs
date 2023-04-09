using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class StavkaRacunaMapper : Profile
    {
        public StavkaRacunaMapper()
        {
            CreateMap<StavkaRacunaEntity, StavkaRacunaDTO>();
            CreateMap<StavkaRacunaCreateDTO, StavkaRacunaEntity>();
            CreateMap<StavkaRacunaUpdateDTO, StavkaRacunaEntity>();
            CreateMap<StavkaRacunaDTO, StavkaRacunaEntity>();
            CreateMap<StavkaRacunaEntity, StavkaRacunaEntity>();
        }
    }
}
