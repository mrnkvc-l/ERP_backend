using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("slika")]
    public class SlikaEntity
    {
        [Key]
        public int IDSlika { get; set; }

        public string Adresa { get; set; } = null!;

        public string Naziv { get; set; } = null!;


        [ForeignKey("Info")]
        [Column("IDProizvodInfo")]
        public int IDInfo { get; set; }

        public InfoEntity Info { get; set; } = null!;
    }
}
