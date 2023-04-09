using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public ProizvodRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ProizvodDTO CreateProizvod(ProizvodCreateDTO proizvodCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteProizvod(int proizvodID)
        {
            throw new NotImplementedException();
        }

        public List<ProizvodEntity> GetAllProizvodi()
        {
            return context.Proizvodi.ToList();
        }

        public ProizvodEntity? GetProizvodByID(int proizvodID)
        {
            return context.Proizvodi.FirstOrDefault(e => e.IDProizvod == proizvodID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
