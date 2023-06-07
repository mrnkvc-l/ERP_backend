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
            StavkaRacunaEntity stavkaRacuna = mapper.Map<StavkaRacunaEntity>(stavkaRacunaCreateDTO);
            context.Add(stavkaRacuna);
            return mapper.Map<StavkaRacunaDTO>(stavkaRacuna);
        }

        public void DeleteStavkaRacuna(int stavkaRacunaID, int racunID)
        {
            StavkaRacunaEntity? stavkaRacuna = GetStavkaRacunaByID(stavkaRacunaID, racunID);
            if(stavkaRacuna != null)
                context.Remove(stavkaRacuna);
        }

        public List<StavkaRacunaEntity> GetAllStavkeRacuna(int racunID)
        {
            return context.StavkeRacuna.Where(o => o.IDRacun == racunID).ToList();
        }

        public StavkaRacunaEntity? GetStavkaRacunaByID(int stavkaRacunaID, int racunID)
        {
            return context.StavkeRacuna.FirstOrDefault(e => e.IDRacun == racunID || e.IDStavkaRacuna == stavkaRacunaID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
