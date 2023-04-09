using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class ProizvodSlikaMapper : Profile
    {
        public ProizvodSlikaMapper()
        {
            CreateMap<ProizvodSlikaEntity, ProizvodSlikaDTO>();
            CreateMap<ProizvodSlikaCreateDTO, ProizvodSlikaEntity>();
            CreateMap<ProizvodSlikaUpdateDTO, ProizvodSlikaEntity>();
            CreateMap<ProizvodSlikaDTO, ProizvodSlikaEntity>();
            CreateMap<ProizvodSlikaEntity, ProizvodSlikaEntity>();
        }
    }
}
