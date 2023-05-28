using AutoMapper;
using ERP_backend.Entity;
using ERP_backend.Model;
using System.Security.Cryptography;

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
            Tuple<string, string> hashPassword = HashPassword(korisnikCreateDTO.Password);
            KorisnikEntity user = mapper.Map<KorisnikEntity>(korisnikCreateDTO);
            user.Password = hashPassword.Item1;
            user.So = hashPassword.Item2;
            user.TipKorisnika = "USER";
            context.Add(user);
            return mapper.Map<KorisnikDTO>(user);
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

        public KorisnikDTO UpdateKorisnik(KorisnikUpdateDTO korisnikUpdateDTO)
        {
            KorisnikEntity? oldUser = context.Korisnici.FirstOrDefault(e => e.IDKorisnik == korisnikUpdateDTO.IDKorisnik);
            KorisnikEntity user = mapper.Map<KorisnikEntity>(korisnikUpdateDTO);
            Tuple<string, string> hashPassword = HashPassword(korisnikUpdateDTO.Password);
            user.Password = hashPassword.Item1;
            user.So = hashPassword.Item2;
            mapper.Map(user, oldUser);
            return mapper.Map<KorisnikDTO>(user);

        }

        private static Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            RandomNumberGenerator.Create().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, 1000);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }

    }
}
