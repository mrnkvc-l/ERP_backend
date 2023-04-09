namespace ERP_backend.Model
{
    public class ProizvodDTO
    {
        public int IDProizvod { get; set; }

        public Boolean Stanje { get; set; }

        public int UkupnaKolicina { get; set; }

        public InfoDTO ProizvodInfo { get; set; } = null!;

        public ProizvodjacDTO Proizvodjac { get; set; } = null!;

        public VelicinaDTO Velicina { get; set; } = null!;

        public KategorijaDTO Kategorija { get; set; } = null!;

        public KolekcijaDTO Kolekcija { get; set; } = null!;
    }
}
