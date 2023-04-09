using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Mappers
{
    public class ProizvodSkladisteMapper : Profile
    {
        public ProizvodSkladisteMapper()
        {
            CreateMap<ProizvodSkladisteEntity, ProizvodSkladisteDTO>();
            CreateMap<ProizvodSkladisteCreateDTO, ProizvodSkladisteEntity>();
            CreateMap<ProizvodSkladisteUpdateDTO, ProizvodSkladisteEntity>();
            CreateMap<ProizvodSkladisteDTO, ProizvodSkladisteEntity>();
            CreateMap<ProizvodSkladisteEntity, ProizvodSkladisteEntity>();
        } 

    }
}
