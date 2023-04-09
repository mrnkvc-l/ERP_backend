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
            throw new NotImplementedException();
        }

        public void DeleteStavkaKorpe(int stavkaKorpeID)
        {
            throw new NotImplementedException();
        }

        public List<StavkaKorpeEntity> GetAllStavkeKorpe(int userID)
        {
            return context.StavkeKorpe.Where(o => o.Kupac == userID).ToList();
        }

        public StavkaKorpeEntity? GetStavkaKorpeByID(int stavkaKorpeID)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
