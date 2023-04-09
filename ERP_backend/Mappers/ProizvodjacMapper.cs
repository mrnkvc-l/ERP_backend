using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class ProizvodjacMapper : Profile
    {
        public ProizvodjacMapper()
        {
            CreateMap<ProizvodjacEntity, ProizvodjacDTO>();
            CreateMap<ProizvodjacCreateDTO, ProizvodjacEntity>();
            CreateMap<ProizvodjacUpdateDTO, ProizvodjacEntity>();
            CreateMap<ProizvodjacDTO,ProizvodjacEntity>();
            CreateMap<ProizvodjacEntity, ProizvodjacEntity>();
        }
    }
}
