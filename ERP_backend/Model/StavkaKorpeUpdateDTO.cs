using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class StavkaKorpeUpdateDTO
    {
        [Required(ErrorMessage = "Proizvod je neophodan!")]
        public int Proizvod { get; set; }

        [Required(ErrorMessage = "Kupac je neophodan!")]
        public int Kupac { get; set; }

        [Required(ErrorMessage = "Kolicina je neophodna!")]
        public int Kolicina { get; set; }
    }
}
