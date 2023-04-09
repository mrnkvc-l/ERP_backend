using ERP_backend.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("stavkaKorpe")]
    [PrimaryKey(nameof(Proizvod), nameof(Kupac))]
    public class StavkaKorpeEntity
    {
        public int Proizvod { get; set; }

        public int Kupac { get; set; }

        public int Kolicina { get; set; }
    }
}
