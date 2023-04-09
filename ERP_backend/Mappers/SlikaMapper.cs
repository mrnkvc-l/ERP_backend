using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class SlikaMapper : Profile
    {
        public SlikaMapper()
        {
            CreateMap<SlikaEntity, SlikaDTO>();
            CreateMap<SlikaCreateDTO, SlikaEntity>();
            CreateMap<SlikaUpdateDTO, SlikaEntity>();
            CreateMap<SlikaDTO, SlikaEntity>();
            CreateMap<SlikaEntity, SlikaEntity>();
        }

    }
}
