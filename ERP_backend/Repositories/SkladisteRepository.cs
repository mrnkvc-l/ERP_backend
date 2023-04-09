using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class SkladisteRepository : ISkladisteRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public SkladisteRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public SkladisteDTO CreateSkladiste(SkladisteCreateDTO skladisteCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteSkladiste(int skladisteID)
        {
            throw new NotImplementedException();
        }

        public List<SkladisteEntity> GetAllSkladista()
        {
            return context.Skladista.ToList();
        }

        public SkladisteEntity? GetSkladisteByID(int skladisteID)
        {
            return context.Skladista.FirstOrDefault(e => e.IDSkladiste == skladisteID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
