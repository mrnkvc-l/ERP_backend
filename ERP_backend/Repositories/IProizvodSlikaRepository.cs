using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IProizvodSlikaRepository
    {
        List<ProizvodSlikaEntity> GetAllProizvodiSlike();

        ProizvodSlikaEntity? GetProizvodSlikaByID(int proizvodSlikaID);

        ProizvodSlikaDTO CreateProizvodSlika(ProizvodSlikaCreateDTO proizvodSlikaCreateDTO);

        void DeleteProizvodSlika(int proizvodSlikaID);

        bool SaveChanges();
    }
}
