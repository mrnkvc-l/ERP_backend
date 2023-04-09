namespace ERP_backend.Model
{
    public class StavkaKorpeDTO
    {
        public ProizvodDTO Proizvod { get; set; } = null!;

        public KorisnikDTO Kupac { get; set; } = null!;

        public int Kolicina { get; set; }
    }
}
