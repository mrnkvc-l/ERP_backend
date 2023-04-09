using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class KolekcijaRepository : IKolekcijaRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public KolekcijaRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public KolekcijaDTO CreateKolekcija(KolekcijaCreateDTO kolekcijaCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteKolekcija(int kolekcijaID)
        {
            throw new NotImplementedException();
        }

        public List<KolekcijaEntity> GetAllKolekcije()
        {
            return context.Kolekcije.ToList();
        }

        public KolekcijaEntity? GetKolekcijaByID(int kolekcijaID)
        {
            return context.Kolekcije.FirstOrDefault(e => e.IDKolekcija == kolekcijaID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
