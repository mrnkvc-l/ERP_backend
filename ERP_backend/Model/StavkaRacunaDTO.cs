﻿namespace ERP_backend.Model
{
    public class StavkaRacunaDTO
    {
        public RacunDTO Racun { get; set; } = null!;

        public int IDStavkaRacuna { get; set; }

        public ProizvodDTO Proizvod { get; set; } = null!;

        public int Kolicina { get; set; }

        public double UkupnaCena { get; set; }
    }
}
