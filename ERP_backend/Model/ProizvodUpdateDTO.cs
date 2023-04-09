﻿using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class ProizvodUpdateDTO
    {
        [Required(ErrorMessage = "ID je neophodan!")]
        public int IDProizvod { get; set; }

        [Required(ErrorMessage = "Stanje je neophodno!")]
        public Boolean Stanje { get; set; }

        [Required(ErrorMessage = "Ukupna kolicina je neophodna!")]
        public int UkupnaKolicina { get; set; }

        [Required(ErrorMessage = "Proizvod je neophodan!")]
        public int IDProizvodInfo { get; set; }

        [Required(ErrorMessage = "Proizvodjac je neophodan!")]
        public int IDProizvodjac { get; set; }

        [Required(ErrorMessage = "Velicina je neophodna!")]
        public int IDVelicina { get; set; }

        [Required(ErrorMessage = "Kategorija je neophodna!")]
        public int IDKategorija { get; set; }

        [Required(ErrorMessage = "Kolekcija je neophodna!")]
        public int IDKolekcija { get; set; }
    }
}
