using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IKategorijaRepository
    {
        List<KategorijaEntity> GetAllKategorije();

        KategorijaEntity? GetKategorijaByID(int kategorijaID);

        KategorijaDTO CreateKategorija(KategorijaCreateDTO kategorijaCreateDTO);

        void DeleteKategorija(int kategorijaID);

        bool SaveChanges();
    }
}
