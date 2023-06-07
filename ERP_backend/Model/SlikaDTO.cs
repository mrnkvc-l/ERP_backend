namespace ERP_backend.Model
{
    public class SlikaDTO
    {
        public int IDSlika { get; set; }

        public InfoDTO Proizvod { get; set; } = null!;

        public string Adresa { get; set; } = null!;

        public string Naziv { get; set; } = null!;
    }
}
