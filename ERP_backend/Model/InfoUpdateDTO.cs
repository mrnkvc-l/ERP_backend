using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class InfoUpdateDTO
    {
        [Required(ErrorMessage ="ID je neophodan!")]
        public int IDInfo { get; set; }

        [Required(ErrorMessage = "Naziv je neophodan!")]
        [MaxLength(50, ErrorMessage = "Duzina mora biti do 50 karaktera!")]
        public string Naziv { get; set; } = null!;

        [MaxLength(300, ErrorMessage = "Duzina mora biti do 300 karaktera!")]
        public string Opis { get; set; } = null!;

        [Required(ErrorMessage = "Cena je neophodna!")]
        public decimal Cena { get; set; }

        public int IDKategorija { get; set; }

        public int IDKolekcija { get; set; }

        public int IDProizvodjac { get; set; }
    }
}
