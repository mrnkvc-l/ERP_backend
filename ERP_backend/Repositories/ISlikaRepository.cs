using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface ISlikaRepository
    {
        List<SlikaEntity> GetAllSlike();

        SlikaEntity? GetSlikaByID(int slikaID);

        SlikaDTO CreateSlika(SlikaCreateDTO slikaCreateDTO);

        void DeleteSlika(int slikaID);

        bool SaveChanges();
    }
}
