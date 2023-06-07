using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class SlikaRepository : ISlikaRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public SlikaRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public SlikaDTO CreateSlika(SlikaCreateDTO slikaCreateDTO)
        {
            SlikaEntity slika = mapper.Map<SlikaEntity>(slikaCreateDTO);
            context.Add(slika);
            return mapper.Map<SlikaDTO>(slika);
        }

        public void DeleteSlika(int slikaID)
        {
            SlikaEntity? slika = GetSlikaByID(slikaID);
            if (slika != null)
                context.Remove(slika);
        }

        public List<SlikaEntity> GetAllSlike()
        {
            return context.Slike.ToList();
        }

        public SlikaEntity? GetSlikaByID(int slikaID)
        {
            return context.Slike.FirstOrDefault(e => e.IDSlika == slikaID);
        }

        public List<SlikaEntity> GetSlikeByProizvod(int IDProizvodInfo)
        {
            return context.Slike.Where(o => o.IDInfo== IDProizvodInfo).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
