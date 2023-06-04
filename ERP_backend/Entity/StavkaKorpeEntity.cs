using ERP_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("stavkaKorpe")]
    [PrimaryKey(nameof(IDProizvod), nameof(IDKupac))]
    public class StavkaKorpeEntity
    {
        [ForeignKey("Proizvod")]
        public int IDProizvod { get; set; }

        public ProizvodEntity Proizvod { get; set; } = null!;

        [ForeignKey("Kupac")]
        public int IDKupac { get; set; }

        public KorisnikEntity Kupac { get; set; } = null!;

        public int Kolicina { get; set; }
    }
}
