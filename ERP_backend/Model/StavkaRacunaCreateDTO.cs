using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class StavkaRacunaCreateDTO
    {
        [Required(ErrorMessage = "Racun je neophodan!")]
        public int Racun { get; set; }

        [Required(ErrorMessage = "Proizvod je neophodan!")]
        public int Proizvod { get; set; }

        [Required(ErrorMessage = "Kolicina je neophodna!")]
        public int Kolicina { get; set; }

        [Required(ErrorMessage = "Ukupna cena je neophodna!")]
        public double UkupnaCena { get; set; }
    }
}
