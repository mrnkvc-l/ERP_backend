using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class UserAuthenticationDTO
    {
        [Required(ErrorMessage = "Korisnik mora da ima username.")]
        [MaxLength(15, ErrorMessage = "Username ne moze biti duzi od 15 karaktera.")]
        public string Username { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Korisnik mora da ima password.")]
        [MinLength(5, ErrorMessage = "Password ne moze biti kraci od 5 karaktera.")]
        [MaxLength(25, ErrorMessage = "Password ne moze biti duzi od 25 karaktera.")]
        public string Password { get; set; } = string.Empty!;
    }
}
