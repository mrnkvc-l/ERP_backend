namespace ERP_backend.Model
{
    public class ProizvodSkladisteDTO
    {
        public ProizvodDTO Proizvod { get; set; } = null!;

        public SkladisteDTO Skladiste { get; set; } = null!;

        public int Kolicina { get; set; }
    }
}
