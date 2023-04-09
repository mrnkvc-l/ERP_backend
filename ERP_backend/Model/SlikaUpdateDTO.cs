using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class SlikaUpdateDTO
    {
        [Required(ErrorMessage = "ID je neophodan!")]
        public int IDSlika { get; set; }

        [Required(ErrorMessage = "Adresa je neophodna!")]
        [MaxLength(100, ErrorMessage = "Duzina mora biti do 100 karaktera!")]
        public string Adresa { get; set; } = null!;

        [Required(ErrorMessage = "Naziv je neophodan!")]
        [MaxLength(50, ErrorMessage = "Duzina mora biti do 50 karaktera!")]
        public string Naziv { get; set; } = null!;

        [Required(ErrorMessage = "Tip slike je neophodan!")]
        [MaxLength(10, ErrorMessage = "Duzina mora biti do 10 karaktera!")]
        public string Tip { get; set; } = null!;
    }
}
