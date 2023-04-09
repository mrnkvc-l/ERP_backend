using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class ProizvodjacCreateDTO
    {
        [Required(ErrorMessage = "Naziv je neophodan!")]
        [MaxLength(50, ErrorMessage = "Duzina mora biti do 50 karaktera!")]
        public string Naziv { get; set; } = null!;

        [Required(ErrorMessage = "Adresa je neophodna!")]
        [MaxLength(200, ErrorMessage = "Duzina mora biti do 200 karaktera!")]
        public string Adresa { get; set; } = null!;
    }
}
