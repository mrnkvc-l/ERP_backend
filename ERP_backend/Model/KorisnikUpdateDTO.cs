﻿using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class KorisnikUpdateDTO
    {
        [Required(ErrorMessage = "ID je neophodan!")]
        public int IDKorisnik { get; set; }

        [Required(ErrorMessage = "Ime je neophodno!")]
        [MaxLength(15, ErrorMessage = "Duzina mora biti do 15 karaktera!")]
        public string Ime { get; set; } = null!;

        [Required(ErrorMessage = "Prezime je neophodno!")]
        [MaxLength(20, ErrorMessage = "Duzina mora biti do 20 karaktera!")]
        public string Prezime { get; set; } = null!;

        [Required(ErrorMessage = "Tip korisnika je neophodan!")]
        [MaxLength(20, ErrorMessage = "Duzina mora biti do 20 karaktera!")]
        public string TipKorisnika { get; set; } = null!;

        [Required(ErrorMessage = "Username je neophodan!")]
        [MaxLength(25, ErrorMessage = "Duzina mora biti do 25 karaktera!")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password je neophodan!")]
        [MaxLength(25, ErrorMessage = "Duzina mora biti do 25 karaktera!")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "So je neophodna!")]
        public string So { get; set; } = null!;

        [Required(ErrorMessage = "Email je neophodan!")]
        [MaxLength(50, ErrorMessage = "Duzina mora biti do 50 karaktera!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Adresa je neophodna!")]
        [MaxLength(50, ErrorMessage = "Duzina mora biti do 50 karaktera!")]
        public string Adresa { get; set; } = null!;

        [Required(ErrorMessage = "Grad je neophodan!")]
        [MaxLength(25, ErrorMessage = "Duzina mora biti do 25 karaktera!")]
        public string Grad { get; set; } = null!;
    }
}
