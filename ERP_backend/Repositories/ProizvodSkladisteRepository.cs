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
            throw new NotImplementedException();
        }

        public void DeleteProizvodSkladiste(int proizvodSkladisteID)
        {
            throw new NotImplementedException();
        }

        public List<ProizvodSkladisteEntity> GetAllProizvodiSkladista()
        {
            return context.ProizvodSkladista.ToList();
        }

        public ProizvodSkladisteEntity? GetProizvodSkladisteByID(int proizvodSkladisteID)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
