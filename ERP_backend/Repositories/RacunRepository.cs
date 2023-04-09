using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class RacunRepository : IRacunRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public RacunRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public RacunDTO CreateRacun(RacunCreateDTO racunCreateDTO)
        {
            RacunEntity racun = mapper.Map<RacunEntity>(racunCreateDTO);
            context.Add(racun);
            return mapper.Map<RacunDTO>(racun);
        }

        public void DeleteRacun(int racunID)
        {
            RacunEntity? racun = GetRacunByID(racunID);
            if (racun != null)
                context.Remove(racun);
        }

        public List<RacunEntity> GetAllRacuni()
        {
            return context.Racuni.ToList();
        }

        public RacunEntity? GetRacunByID(int racunID)
        {
            return context.Racuni.FirstOrDefault(e => e.IDRacun == racunID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
