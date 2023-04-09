using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IVelicinaRepository
    {
        List<VelicinaEntity> GetAllVelicine();

        VelicinaEntity? GetVelicinaByID(int velicinaID);

        VelicinaDTO CreateVelicina(VelicinaCreateDTO velicinaCreateDTO);

        void DeleteVelicina(int velicinaID);

        bool SaveChanges();
    }
}
