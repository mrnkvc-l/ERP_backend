using ERP_backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("proizvod")]
    public class ProizvodEntity
    {
        [Key]
        public int IDProizvod { get; set; }

        [Required]
        public Boolean Stanje { get; set; }

        [Required]
        public int UkupnaKolicina { get; set; }

        [Required]
        [Column("IDInfo")]
        [ForeignKey("Info")]
        public int IDProizvodInfo { get; set; }

        [Required]
        public InfoEntity Info { get; set; } = null!;

        [Required]
        [ForeignKey("Proizvodjac")]
        public int IDProizvodjac { get; set; }

        [Required]
        public ProizvodjacEntity Proizvodjac { get; set; } = null!;

        [Required]
        public int IDVelicina { get; set; }

        [Required]
        public int IDKategorija { get; set; }

        [Required]
        public int IDKolekcija { get; set; }
    }
}
