using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public ProizvodRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProizvodDTO CreateProizvod(ProizvodCreateDTO proizvodCreateDTO)
        {
            ProizvodEntity proizvod = mapper.Map<ProizvodEntity>(proizvodCreateDTO);
            context.Add(proizvod);
            return mapper.Map<ProizvodDTO>(proizvod);
        }

        public void DeleteProizvod(int proizvodID)
        {
            ProizvodEntity? proizvod = GetProizvodByID(proizvodID);
            if(proizvod != null)
                context.Remove(proizvod);
        }

        public List<ProizvodEntity> GetAllProizvodi()
        {
            return context.Proizvodi.ToList();
        }

        public ProizvodEntity? GetProizvodByID(int proizvodID)
        {
            return context.Proizvodi.FirstOrDefault(e => e.IDProizvod == proizvodID);
        }

        public List<ProizvodEntity> GetProizvodByInfo(int infoID)
        {
            return context.Proizvodi.Where(o => o.IDProizvodInfo == infoID).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
