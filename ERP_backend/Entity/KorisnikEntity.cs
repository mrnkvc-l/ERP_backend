using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_backend.Entity
{
    [Table("korisnik")]
    public class KorisnikEntity
    {
        [Key]
        public int IDKorisnik { get; set; }

        [Required]
        public string Ime { get; set; } = null!;

        [Required]
        public string Prezime { get; set; } = null!;

        [Required]
        public string TipKorisnika { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Adresa { get; set; } = null!;

        [Required]
        public string Grad { get; set; } = null!;
    }
}
