using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IStavkaRacunaRepository
    {
        List<StavkaRacunaEntity> GetAllStavkeRacuna(int racunID);

        StavkaRacunaEntity? GetStavkaRacunaByID(int stavkaRacunaID, int racunID);

        StavkaRacunaDTO CreateStavkaRacuna(StavkaRacunaCreateDTO stavkaRacunaCreateDTO);

        void DeleteStavkaRacuna(int stavkaRacunaID, int racunID);

        bool SaveChanges();
    }
}
