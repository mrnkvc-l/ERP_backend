using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class StavkaRacunaRepository : IStavkaRacunaRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public StavkaRacunaRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public StavkaRacunaDTO CreateStavkaRacuna(StavkaRacunaCreateDTO stavkaRacunaCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteStavkaRacuna(int stavkaRacunaID)
        {
            throw new NotImplementedException();
        }

        public List<StavkaRacunaEntity> GetAllStavkeRacuna(int racunID)
        {
            throw new NotImplementedException();
        }

        public StavkaRacunaEntity? GetStavkaRacunaByID(int stavkaRacunaID)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
