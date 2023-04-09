using ERP_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("stavkaRacuna")]
    [PrimaryKey(nameof(Racun), nameof(IDStavkaRacuna))]
    public class StavkaRacunaEntity
    {
        public int Racun { get; set; }

        public int IDStavkaRacuna { get; set; }

        public int Proizvod { get; set; }

        public int Kolicina { get; set; }

        public double UkupnaCena { get; set; }
    }
}
