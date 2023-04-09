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
            throw new NotImplementedException();
        }

        public void DeleteSlika(int slikaID)
        {
            throw new NotImplementedException();
        }

        public List<SlikaEntity> GetAllSlike()
        {
            return context.Slike.ToList();
        }

        public SlikaEntity? GetSlikaByID(int slikaID)
        {
            return context.Slike.FirstOrDefault(e => e.IDSlika == slikaID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
