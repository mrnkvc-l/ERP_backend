using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class ProizvodSkladisteRepository : IProizvodSkladisteRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public ProizvodSkladisteRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProizvodSkladisteDTO CreateProizvodSkladiste(ProizvodSkladisteCreateDTO proizvodSkladisteCreateDTO)
        {
            ProizvodSkladisteEntity entity = mapper.Map<ProizvodSkladisteEntity>(proizvodSkladisteCreateDTO);
            context.Add(entity);
            return mapper.Map<ProizvodSkladisteDTO>(entity);
        }

        public void DeleteProizvodSkladiste(int proizvodID, int skladisteID)
        {
            ProizvodSkladisteEntity? proizvodSkladiste = GetProizvodSkladisteByID(proizvodID,skladisteID);
            if (proizvodSkladiste != null)
                context.Remove(proizvodSkladiste);
        }

        public List<ProizvodSkladisteEntity> GetAllProizvodiSkladista(int proizvodID)
        {
            return context.ProizvodSkladista.Where(o => o.Proizvod == proizvodID).ToList();
        }

        public ProizvodSkladisteEntity? GetProizvodSkladisteByID(int proizvodID, int skladisteID)
        {
            return context.ProizvodSkladista.FirstOrDefault(e => e.Proizvod == proizvodID || e.Skladiste == skladisteID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
