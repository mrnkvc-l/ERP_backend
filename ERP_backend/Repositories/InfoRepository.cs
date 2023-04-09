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
            InfoEntity info = mapper.Map<InfoEntity>(infoCreateDTO);
            context.Add(info);
            return mapper.Map<InfoDTO>(info);
        }

        public void DeleteInfo(int infoID)
        {
            InfoEntity? info = GetInfoByID(infoID);
            if (info != null)
                context.Remove(info);
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
