using ERP_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("stavkaRacuna")]
    [PrimaryKey(nameof(IDRacun), nameof(IDStavkaRacuna))]
    public class StavkaRacunaEntity
    {
        [ForeignKey("Racun")]
        public int IDRacun { get; set; }

        public RacunEntity Racun { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDStavkaRacuna { get; set; }

        [ForeignKey("Proizvod")]
        public int IDProizvod { get; set; }

        public ProizvodEntity Proizvod { get; set; } = null!;

        public int Kolicina { get; set; }

        public decimal Cena { get; set; }

        public decimal Popust { get; set; }
    }
}
