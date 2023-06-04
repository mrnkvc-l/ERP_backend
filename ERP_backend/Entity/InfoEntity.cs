using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("proizvodInfo")]
    public class InfoEntity
    {
        [Key]
        public int IDInfo { get; set; }

        [Required]
        public string Naziv { get; set; } = null!;

        [Required]
        public string Opis { get; set; } = null!;

        [Required]
        public Boolean Stanje { get; set; }

        [Required]
        public decimal Cena { get; set; }

        [Required]
        public decimal Popust { get; set; }

        [ForeignKey("Kategorija")]
        public int IDKategorija { get; set; }

        [Required]
        public KategorijaEntity Kategorija { get; set; } = null!;

        [ForeignKey("Kolekcija")]
        public int IDKolekcija { get; set; }

        [Required]
        public KolekcijaEntity Kolekcija { get; set; } = null!;

        [ForeignKey("Proizvodjac")]
        public int IDProizvodjac { get; set; }

        [Required]
        public ProizvodjacEntity Proizvodjac { get; set; } = null!;
    }
}
