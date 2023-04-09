using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class ProizvodSlikaRepository : IProizvodSlikaRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public ProizvodSlikaRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProizvodSlikaDTO CreateProizvodSlika(ProizvodSlikaCreateDTO proizvodSlikaCreateDTO)
        {
            ProizvodSlikaEntity entity = mapper.Map<ProizvodSlikaEntity>(proizvodSlikaCreateDTO);
            context.Add(entity);
            return mapper.Map<ProizvodSlikaDTO>(entity);
        }

        public void DeleteProizvodSlika(int proizvodID, int slikaID)
        {
            ProizvodSlikaEntity? proizvodSlika = GetProizvodSlikaByID(proizvodID, slikaID);
            if (proizvodSlika != null)
                context.Remove(proizvodSlika);
        }

        public List<ProizvodSlikaEntity> GetAllProizvodiSlike(int proizvodID)
        {
            return context.ProizvodSlike.Where(o => o.Proizvod == proizvodID).ToList();
        }

        public ProizvodSlikaEntity? GetProizvodSlikaByID(int proizvodID, int slikaID)
        {
            return context.ProizvodSlike.FirstOrDefault(e => e.Proizvod == proizvodID || e.Slika == slikaID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
