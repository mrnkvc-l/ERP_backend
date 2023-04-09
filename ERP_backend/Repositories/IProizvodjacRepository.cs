using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IProizvodjacRepository
    {
        List<ProizvodjacEntity> GetAllProizvodjaci();

        ProizvodjacEntity? GetProizvodjacByID(int proizvodjacID);

        ProizvodjacDTO CreateProizvodjac(ProizvodjacCreateDTO proizvodjacCreateDTO);

        void DeleteProizvodjac(int proizvodjacID);

        bool SaveChanges();
    }
}
