using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IProizvodSlikaRepository
    {
        List<ProizvodSlikaEntity> GetAllProizvodiSlike(int proizvodID);

        ProizvodSlikaEntity? GetProizvodSlikaByID(int proizvodID, int slikaID);

        ProizvodSlikaDTO CreateProizvodSlika(ProizvodSlikaCreateDTO proizvodSlikaCreateDTO);

        void DeleteProizvodSlika(int proizvodID, int slikaID);

        bool SaveChanges();
    }
}
