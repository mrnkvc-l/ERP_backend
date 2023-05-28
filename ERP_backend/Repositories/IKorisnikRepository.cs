using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public interface IKorisnikRepository
    {
        List<KorisnikEntity> GetAllKorisnici();

        KorisnikEntity? GetKorisnikByID(int korisnikID);

        KorisnikDTO CreateKorisnik(KorisnikCreateDTO korisnikCreateDTO);

        void DeleteKorisnik(int korisnikID);

        bool SaveChanges();
        KorisnikDTO UpdateKorisnik(KorisnikUpdateDTO korisnikUpdateDTO);
    }
}
