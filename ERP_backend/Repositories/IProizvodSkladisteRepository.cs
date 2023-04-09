using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IProizvodSkladisteRepository
    {
        List<ProizvodSkladisteEntity> GetAllProizvodiSkladista(int proizvodID);

        ProizvodSkladisteEntity? GetProizvodSkladisteByID(int proizvodID, int skladisteID);

        ProizvodSkladisteDTO CreateProizvodSkladiste(ProizvodSkladisteCreateDTO proizvodSkladisteCreateDTO);

        void DeleteProizvodSkladiste(int proizvodID, int skladisteID);

        bool SaveChanges();
    }
}
