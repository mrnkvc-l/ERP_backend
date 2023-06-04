namespace ERP_backend.Model
{
    public class InfoDTO
    {
        public int IDInfo { get; set; }

        public string Naziv { get; set; } = null!;

        public string Opis { get; set; } = null!;

        public Boolean Stanje { get; set; }

        public decimal Popust { get; set; }

        public decimal Cena { get; set; }

        public KategorijaDTO Kategorija { get; set; } = null!;

        public KolekcijaDTO Kolekcija { get; set;} = null!;

        public ProizvodjacDTO Proizvodjac { get; set;} = null!;
    }
}
