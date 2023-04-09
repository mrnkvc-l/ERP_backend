using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class KategorijaRepository : IKategorijaRepository
    {

        private readonly ErpContext context;
        private readonly IMapper mapper;

        public KategorijaRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public KategorijaDTO CreateKategorija(KategorijaCreateDTO kategorijaCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteKategorija(int kategorijaID)
        {
            throw new NotImplementedException();
        }

        public List<KategorijaEntity> GetAllKategorije()
        {
            return context.Kategorije.ToList();
        }

        public KategorijaEntity? GetKategorijaByID(int kategorijaID)
        {
            return context.Kategorije.FirstOrDefault(e => e.IDKategorija == kategorijaID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
