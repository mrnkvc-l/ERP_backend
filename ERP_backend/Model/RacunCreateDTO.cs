using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class RacunCreateDTO
    {
        [Required(ErrorMessage = "Kupac je neophodan!")]
        public int Kupac { get; set; }

        [Required(ErrorMessage = "Ukupna cena je neophodna!")]
        public double UkupnaCena { get; set; }

        [Required(ErrorMessage = "Datum je neophodan!")]
        public DateTime Datum { get; set; }
    }
}
