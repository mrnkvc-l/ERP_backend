using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("kolekcija")]
    [PrimaryKey(nameof(IDKolekcija))]
    public class KolekcijaEntity
    {
        [Key]
        public int IDKolekcija { get; set; }

        [Required]
        public string Naziv { get; set; } = null!;

        [Required]
        public string Opis { get; set; } = null!;
    }
}
