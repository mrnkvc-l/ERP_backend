using ERP_backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("racun")]
    public class RacunEntity
    {
        [Key]
        public int IDRacun { get; set; }

        [Required]
        [ForeignKey("Kupac")]
        public int IDKupac { get; set; }

        [Required]
        public KorisnikEntity Kupac { get; set; } = null!;

        [Required]
        public decimal UkupnaCena { get; set; }

        [Required]
        public DateTime Datum { get; set; }
    }
}
