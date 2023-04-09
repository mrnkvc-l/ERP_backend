using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IRacunRepository
    {
        List<RacunEntity> GetAllRacuni();

        RacunEntity? GetRacunByID(int racunID);

        RacunDTO CreateRacun(RacunCreateDTO racunCreateDTO);

        void DeleteRacun(int racunID);

        bool SaveChanges();
    }
}
