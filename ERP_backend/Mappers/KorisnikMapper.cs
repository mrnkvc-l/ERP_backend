using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class KorisnikMapper : Profile
    {
        public KorisnikMapper()
        {
            CreateMap<KorisnikEntity,KorisnikDTO>();
            CreateMap<KorisnikCreateDTO, KorisnikEntity>();
            CreateMap<KorisnikUpdateDTO, KorisnikEntity>();
            CreateMap<KorisnikDTO, KorisnikEntity>();
            CreateMap<KorisnikEntity,KorisnikEntity>();
        }
    }
}
