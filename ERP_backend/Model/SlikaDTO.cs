namespace ERP_backend.Model
{
    public class SlikaDTO
    {
        public int IDSlika { get; set; }

        public ProizvodDTO Proizvod { get; set; } = null!;

        public string Adresa { get; set; } = null!;

        public string Naziv { get; set; } = null!;

        public string Tip { get; set; } = null!;
    }
}
