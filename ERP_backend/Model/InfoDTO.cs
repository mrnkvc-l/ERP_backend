namespace ERP_backend.Model
{
    public class InfoDTO
    {
        public int IDInfo { get; set; }

        public string Naziv { get; set; } = null!;

        public string Opis { get; set; } = null!;

        public decimal Cena { get; set; }
    }
}
