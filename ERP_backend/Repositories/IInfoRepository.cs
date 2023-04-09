using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IInfoRepository
    {
        List<InfoEntity> GetAllInfo();

        InfoEntity? GetInfoByID(int infoID);

        InfoDTO CreateInfo(InfoCreateDTO infoCreateDTO);

        void DeleteInfo(int infoID);

        bool SaveChanges();
    }
}
