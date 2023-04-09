using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class VelicinaUpdateDTO
    {
        [Required(ErrorMessage = "ID je neophodan!")]
        public int IDVelicina { get; set; }

        [Required(ErrorMessage = "Oznaka je neophodna!")]
        [MaxLength(10, ErrorMessage = "Duzina mora biti do 10 karaktera!")]
        public string Oznaka { get; set; } = null!;
    }
}
