using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IProizvodSkladisteRepository
    {
        List<ProizvodSkladisteEntity> GetAllProizvodiSkladista();

        ProizvodSkladisteEntity? GetProizvodSkladisteByID(int proizvodSkladisteID);

        ProizvodSkladisteDTO CreateProizvodSkladiste(ProizvodSkladisteCreateDTO proizvodSkladisteCreateDTO);

        void DeleteProizvodSkladiste(int proizvodSkladisteID);

        bool SaveChanges();
    }
}
