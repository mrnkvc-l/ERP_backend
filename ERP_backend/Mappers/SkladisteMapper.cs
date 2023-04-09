using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class SkladisteMapper : Profile
    {
        public SkladisteMapper() { 
            CreateMap<SkladisteEntity, SkladisteDTO>();
            CreateMap<SkladisteCreateDTO, SkladisteEntity>();
            CreateMap<SkladisteUpdateDTO, SkladisteEntity>();
            CreateMap<SkladisteDTO, SkladisteEntity>();
            CreateMap<SkladisteEntity, SkladisteEntity>();
        }
    }
}
