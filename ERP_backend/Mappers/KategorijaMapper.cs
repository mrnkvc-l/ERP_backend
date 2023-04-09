using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class KategorijaMapper : Profile
    {
        public KategorijaMapper()
        {
            CreateMap<KategorijaEntity, KategorijaDTO>();
            CreateMap<KategorijaCreateDTO,KategorijaEntity>();
            CreateMap<KategorijaUpdateDTO,KategorijaEntity>();
            CreateMap<KategorijaDTO,KategorijaEntity>();
            CreateMap<KategorijaEntity,KategorijaEntity>();
        }
    }
}
