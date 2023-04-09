using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IStavkaRacunaRepository
    {
        List<StavkaRacunaEntity> GetAllStavkeRacuna(int racunID);

        StavkaRacunaEntity? GetStavkaRacunaByID(int stavkaRacunaID);

        StavkaRacunaDTO CreateStavkaRacuna(StavkaRacunaCreateDTO stavkaRacunaCreateDTO);

        void DeleteStavkaRacuna(int stavkaRacunaID);

        bool SaveChanges();
    }
}
