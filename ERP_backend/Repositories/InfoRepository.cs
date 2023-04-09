using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class InfoRepository : IInfoRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public InfoRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public InfoDTO CreateInfo(InfoCreateDTO infoCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteInfo(int infoID)
        {
            throw new NotImplementedException();
        }

        public List<InfoEntity> GetAllInfo()
        {
            return context.Informacije.ToList();
        }

        public InfoEntity? GetInfoByID(int infoID)
        {
            return context.Informacije.FirstOrDefault(e => e.IDInfo == infoID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
