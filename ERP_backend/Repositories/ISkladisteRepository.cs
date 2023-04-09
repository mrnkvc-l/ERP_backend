using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface ISkladisteRepository
    {
        List<SkladisteEntity> GetAllSkladista();

        SkladisteEntity? GetSkladisteByID(int skladisteID);

        SkladisteDTO CreateSkladiste(SkladisteCreateDTO skladisteCreateDTO);

        void DeleteSkladiste(int skladisteID);

        bool SaveChanges();
    }
}
