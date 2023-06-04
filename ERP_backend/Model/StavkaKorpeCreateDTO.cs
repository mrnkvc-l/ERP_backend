using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class StavkaKorpeCreateDTO
    {
        [Required(ErrorMessage = "Proizvod je neophodan!")]
        public int IDProizvod { get; set; }

        [Required(ErrorMessage = "Kupac je neophodan!")]
        public int IDKupac { get; set; }

        [Required(ErrorMessage = "Kolicina je neophodna!")]
        public int Kolicina { get; set; }
    }
}
