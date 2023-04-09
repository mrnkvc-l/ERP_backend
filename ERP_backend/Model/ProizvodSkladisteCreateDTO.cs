using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class ProizvodSkladisteCreateDTO
    {
        [Required(ErrorMessage ="Proizvod je neophodan!")]
        public int Proizvod { get; set; }

        [Required(ErrorMessage = "Skladiste je neophodno!")]
        public int Skladiste { get; set; }

        [Required(ErrorMessage = "Kolicina je neophodna!")]
        public int Kolicina { get; set; }
    }
}
