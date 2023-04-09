using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("kategorija")]
    public class KategorijaEntity
    {
        [Key]
        public int IDKategorija { get; set; }

        [Required]
        public string Naziv { get; set; } = null!;

        [Required]
        public string Opis { get; set; } = null!;
    }
}
