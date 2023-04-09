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
            KategorijaEntity kategorija = mapper.Map<KategorijaEntity>(kategorijaCreateDTO);
            context.Add(kategorija);
            return mapper.Map<KategorijaDTO>(kategorija);
        }

        public void DeleteKategorija(int kategorijaID)
        {
            KategorijaEntity? kategorija = GetKategorijaByID(kategorijaID);
            if (kategorija != null)
                context.Remove(kategorija);
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
