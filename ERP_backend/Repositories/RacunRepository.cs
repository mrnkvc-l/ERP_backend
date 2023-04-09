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
            throw new NotImplementedException();
        }

        public void DeleteRacun(int racunID)
        {
            throw new NotImplementedException();
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
