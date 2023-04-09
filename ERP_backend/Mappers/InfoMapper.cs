using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using System.Runtime.CompilerServices;

namespace ERP_backend.Mappers
{
    public class InfoMapper : Profile
    {
        public InfoMapper() {
            CreateMap<InfoEntity, InfoDTO>();
            CreateMap<InfoCreateDTO,InfoEntity>();
            CreateMap<InfoUpdateDTO, InfoEntity>();
            CreateMap<InfoDTO, InfoEntity>();
            CreateMap<InfoEntity,InfoEntity>();
        }
    }
}
