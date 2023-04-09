﻿using System.ComponentModel.DataAnnotations;

namespace ERP_backend.Model
{
    public class ProizvodSlikaCreateDTO
    {
        [Required(ErrorMessage = "Proizvod je neophodan!")]
        public int Proizvod { get; set; }

        [Required(ErrorMessage = "Slika je neophodna!")]
        public int Slika { get; set; }
    }
}