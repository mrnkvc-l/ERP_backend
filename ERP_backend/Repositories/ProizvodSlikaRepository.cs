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
            throw new NotImplementedException();
        }

        public void DeleteProizvodSlika(int proizvodSlikaID)
        {
            throw new NotImplementedException();
        }

        public List<ProizvodSlikaEntity> GetAllProizvodiSlike()
        {
            return context.ProizvodSlike.ToList();
        }

        public ProizvodSlikaEntity? GetProizvodSlikaByID(int proizvodSlikaID)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
