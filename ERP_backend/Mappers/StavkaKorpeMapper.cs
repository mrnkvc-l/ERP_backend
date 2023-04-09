using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class StavkaKorpeMapper : Profile
    {
        public StavkaKorpeMapper()
        {
            CreateMap<StavkaKorpeEntity,StavkaKorpeDTO>();
            CreateMap<StavkaKorpeCreateDTO, StavkaKorpeEntity>();
            CreateMap<StavkaKorpeUpdateDTO, StavkaKorpeEntity>();
            CreateMap<StavkaKorpeDTO, StavkaKorpeEntity>();
            CreateMap<StavkaKorpeEntity, StavkaKorpeEntity>();
        }
    }
}
