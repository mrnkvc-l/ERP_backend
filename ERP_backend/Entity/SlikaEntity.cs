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

        public string Tip { get; set; } = null!;
    }
}
