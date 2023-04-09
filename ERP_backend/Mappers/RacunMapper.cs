using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class RacunMapper : Profile
    {
        public RacunMapper() { 
            CreateMap<RacunEntity, RacunDTO>();
            CreateMap<RacunCreateDTO, RacunEntity>();
            CreateMap<RacunUpdateDTO, RacunEntity>();
            CreateMap<RacunDTO, RacunEntity>();
            CreateMap<RacunEntity, RacunEntity>();
        }
    }
}
