using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class KolekcijaMapper : Profile
    {
        public KolekcijaMapper()
        {
            CreateMap<KolekcijaEntity, KolekcijaDTO>();
            CreateMap<KolekcijaCreateDTO, KolekcijaEntity>();
            CreateMap<KolekcijaUpdateDTO, KolekcijaEntity>();
            CreateMap<KolekcijaDTO, KolekcijaEntity>();
            CreateMap<KolekcijaEntity, KolekcijaEntity>();
        }
    }
}
