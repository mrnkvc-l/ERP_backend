﻿namespace ERP_backend.Model
{
    public class RacunDTO
    {
        public int IDRacun { get; set; }

        public KorisnikDTO Kupac { get; set; } = null!;

        public double UkupnaCena { get; set; }

        public DateTime Datum { get; set; }
    }
}
