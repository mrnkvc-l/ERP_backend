using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class RacunUpdateDTO
    {
        [Required(ErrorMessage = "ID je neophodan!")]
        public int IDRacun { get; set; }

        [Required(ErrorMessage = "Kupac je neophodan!")]
        public int IDKupac { get; set; }

        [Required(ErrorMessage = "Ukupna cena je neophodna!")]
        public decimal UkupnaCena { get; set; }

        [Required(ErrorMessage = "Datum je neophodan!")]
        public DateTime Datum { get; set; }
    }
}
