using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class StavkaKorpeRepository : IStavkaKorpeRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public StavkaKorpeRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public StavkaKorpeDTO CreateStavkaKorpe(StavkaKorpeCreateDTO stavkaKorpeCreateDTO)
        {
            StavkaKorpeEntity stavkaKorpe = mapper.Map<StavkaKorpeEntity>(stavkaKorpeCreateDTO);
            context.Add(stavkaKorpe);
            return mapper.Map<StavkaKorpeDTO>(stavkaKorpe);
        }

        public void DeleteStavkaKorpe(int stavkaKorpeID, int userID)
        {
            StavkaKorpeEntity? stavkaKorpe = GetStavkaKorpeByID(stavkaKorpeID, userID);
            if(stavkaKorpe != null)
                context.Remove(stavkaKorpe);
        }

        public List<StavkaKorpeEntity> GetAllStavkeKorpe(int userID)
        {
            return context.StavkeKorpe.Where(o => o.IDKupac == userID).ToList();
        }

        public StavkaKorpeEntity? GetStavkaKorpeByID(int stavkaKorpeID, int userID)
        {
            return context.StavkeKorpe.FirstOrDefault(e => e.IDKupac == userID || e.IDProizvod == stavkaKorpeID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
