using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IStavkaKorpeRepository
    {
        List<StavkaKorpeEntity> GetAllStavkeKorpe(int userID);

        StavkaKorpeEntity? GetStavkaKorpeByID(int stavkaKorpeID);

        StavkaKorpeDTO CreateStavkaKorpe(StavkaKorpeCreateDTO stavkaKorpeCreateDTO);

        void DeleteStavkaKorpe(int stavkaKorpeID);

        bool SaveChanges();
    }
}
