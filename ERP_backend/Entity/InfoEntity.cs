using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("proizvodInfo")]
    public class InfoEntity
    {
        [Key]
        public int IDInfo { get; set; }

        [Required]
        public string Naziv { get; set; } = null!;

        [Required]
        public string Opis { get; set; } = null!;

        [Required]
        public decimal Cena { get; set; }

        //public List<InfoEntity>? Infos { get; set; } = null!;
    }
}
