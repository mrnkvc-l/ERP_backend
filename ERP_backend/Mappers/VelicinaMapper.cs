using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class VelicinaMapper : Profile
    {
        public VelicinaMapper()
        {
            CreateMap<VelicinaEntity, VelicinaDTO>();
            CreateMap<VelicinaCreateDTO, VelicinaEntity>();
            CreateMap<VelicinaUpdateDTO, VelicinaEntity>();
            CreateMap<VelicinaDTO, VelicinaEntity>();
            CreateMap<VelicinaEntity, VelicinaEntity>();
        }
    }
}
