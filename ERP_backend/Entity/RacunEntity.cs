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

        public int Kupac { get; set; }

        public double UkupnaCena { get; set; }

        public DateTime Datum { get; set; }
    }
}
