using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IKolekcijaRepository
    {
        List<KolekcijaEntity> GetAllKolekcije();

        KolekcijaEntity? GetKolekcijaByID(int kolekcijaID);

        KolekcijaDTO CreateKolekcija(KolekcijaCreateDTO kolekcijaCreateDTO);

        void DeleteKolekcija(int kolekcijaID);

        bool SaveChanges();
    }
}
