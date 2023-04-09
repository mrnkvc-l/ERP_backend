using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("proizvodjac")]
    public class ProizvodjacEntity
    {
        [Key]
        public int IDProizvodjac { get; set; }

        [Required]
        public string Naziv { get; set; } = null!;

        [Required]
        public string Adresa { get; set; } = null!;

    }
}
