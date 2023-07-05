using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace ERP_backend.Entity
{
    public class ErpContext : DbContext
    {
        public ErpContext(DbContextOptions options) : base(options) { }

        public DbSet<KategorijaEntity> Kategorije { get; set; }

        public DbSet<KolekcijaEntity> Kolekcije { get; set; }

        public DbSet<InfoEntity> Informacije { get; set; }

        public DbSet<KorisnikEntity> Korisnici { get;set; }

        public DbSet<ProizvodEntity> Proizvodi { get; set;}

        public DbSet<ProizvodjacEntity> Proizvodjaci { get;set; }

        public DbSet<RacunEntity> Racuni { get; set; }

        public DbSet<SlikaEntity> Slike { get; set; }

        public DbSet<StavkaKorpeEntity> StavkeKorpe { get;set; }

        public DbSet<StavkaRacunaEntity> StavkeRacuna { get;set; }

        public DbSet<VelicinaEntity> Velicine { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProizvodEntity>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"));
        }
    }
}
