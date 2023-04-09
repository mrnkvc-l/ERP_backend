using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class ProizvodMapper : Profile
    {
        public ProizvodMapper()
        {
            CreateMap<ProizvodEntity, ProizvodDTO>();
            CreateMap<ProizvodCreateDTO, ProizvodEntity>();
            CreateMap<ProizvodUpdateDTO, ProizvodEntity>();
            CreateMap<ProizvodDTO, ProizvodEntity>();
            CreateMap<ProizvodEntity, ProizvodEntity>();
        }
    }
}
