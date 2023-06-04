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
        public int UkupnaKolicina { get; set; }

        [Required]
        [Column("IDInfo")]
        [ForeignKey("Info")]
        public int IDProizvodInfo { get; set; }

        [Required]
        public InfoEntity Info { get; set; } = null!;

        [Required]
        [ForeignKey("Velicina")]
        public int IDVelicina { get; set; }

        [Required]
        public VelicinaEntity Velicina { get; set; } = null!;

    }
}
