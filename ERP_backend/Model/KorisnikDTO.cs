namespace ERP_backend.Model
{
    public class KorisnikDTO
    {
        public int IDKorisnik { get; set; }

        public string Ime { get; set; } = null!;

        public string Prezime { get; set; } = null!;

        public string TipKorisnika { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Adresa { get; set; } = null!;

        public string Grad { get; set; } = null!;

    }
}
