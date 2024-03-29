﻿using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class ProizvodCreateDTO
    {
        [Required(ErrorMessage = "Ukupna kolicina je neophodna!")]
        public int UkupnaKolicina { get; set; }

        [Required(ErrorMessage ="Proizvod je neophodan!")]
        public int IDProizvodInfo { get; set; }

        [Required(ErrorMessage = "Velicina je neophodna!")]
        public int IDVelicina { get; set; }

    }
}
