using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;

namespace ERP_backend.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly ErpContext context;
        private readonly IMapper mapper;

        public KorisnikRepository(ErpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public KorisnikDTO CreateKorisnik(KorisnikCreateDTO korisnikCreateDTO)
        {
            KorisnikEntity korisnik = mapper.Map<KorisnikEntity>(korisnikCreateDTO);
            context.Add(korisnik);
            return mapper.Map<KorisnikDTO>(korisnik);
        }

        public void DeleteKorisnik(int korisnikID)
        {
            KorisnikEntity? korisnik = GetKorisnikByID(korisnikID);
            if(korisnik != null)
                context.Remove(korisnik);
        }

        public List<KorisnikEntity> GetAllKorisnici()
        {
            return context.Korisnici.ToList();
        }

        public KorisnikEntity? GetKorisnikByID(int korisnikID)
        {
            return context.Korisnici.FirstOrDefault(e => e.IDKorisnik == korisnikID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
