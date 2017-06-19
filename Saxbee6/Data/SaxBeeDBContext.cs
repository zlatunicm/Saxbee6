using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SaxBee6.Data.Models;
using Microsoft.Extensions.Configuration;

namespace SaxBee6.Data
{
    public partial class SaxBeeDBContext : DbContext
    {
        private IConfigurationRoot _config;

        public SaxBeeDBContext(IConfigurationRoot config, DbContextOptions options): base(options)
        {
            _config = config;
        }

        public virtual DbSet<GeoPolozaj> GeoPolozaj { get; set; }
        public virtual DbSet<KategorizacijaStavkiIzvjesca> KategorizacijaStavkiIzvjesca { get; set; }
        public virtual DbSet<Matica> Matica { get; set; }
        public virtual DbSet<OkviriZajednice> OkviriZajednice { get; set; }
        public virtual DbSet<PZajednica> PZajednica { get; set; }
        public virtual DbSet<Pcelar> Pcelar { get; set; }
        public virtual DbSet<Pcelinjak> Pcelinjak { get; set; }
        public virtual DbSet<Porijeklo> Porijeklo { get; set; }
        public virtual DbSet<RadnoIzvjesce> RadnoIzvjesce { get; set; }
        public virtual DbSet<Radovi> Radovi { get; set; }
        public virtual DbSet<VrstaKosnice> VrstaKosnice { get; set; }
        public virtual DbSet<VrstaPorijekla> VrstaPorijekla { get; set; }
        public virtual DbSet<VrstaRizvjesca> VrstaRizvjesca { get; set; }
        public virtual DbSet<VrsteOkvira> VrsteOkvira { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:DefaultConnection:SaxBeeDBConnectionString"]);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeoPolozaj>(entity =>
            {
                entity.HasKey(e => e.IdGeoPolozaj)
                    .HasName("PK_GeoPolozaj");

                entity.Property(e => e.IdGeoPolozaj).HasColumnName("ID_GeoPolozaj");

                entity.Property(e => e.GeografskaDuzina).HasColumnName("Geografska_Duzina");

                entity.Property(e => e.GeografskaSirina).HasColumnName("Geografska_Sirina");
            });

            modelBuilder.Entity<KategorizacijaStavkiIzvjesca>(entity =>
            {
                entity.HasKey(e => e.IdKategorizacijaStavkiIzvjesca)
                    .HasName("PK_Kategorizacija_stavki_izvjesca");

                entity.ToTable("Kategorizacija_stavki_izvjesca");

                entity.Property(e => e.IdKategorizacijaStavkiIzvjesca).HasColumnName("ID_kategorizacija_stavki_izvjesca");

                entity.Property(e => e.IdPorijekloFk).HasColumnName("ID_Porijeklo_FK");

                entity.Property(e => e.Napomena).HasColumnType("varchar(250)");

                entity.Property(e => e.Opis).HasColumnType("varchar(2000)");

                entity.Property(e => e.OznakaKategorije)
                    .HasColumnName("Oznaka_Kategorije")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Matica>(entity =>
            {
                entity.HasKey(e => e.IdMatice)
                    .HasName("PK_Matica");

                entity.Property(e => e.IdMatice).HasColumnName("ID_Matice");

                entity.Property(e => e.GodisteMatice)
                    .HasColumnName("Godiste_Matice")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPorijekloFk).HasColumnName("ID_Porijeklo_FK");

                entity.Property(e => e.OpisMatice)
                    .HasColumnName("Opis_Matice")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.OznakaMatice)
                    .HasColumnName("Oznaka_Matice")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdPorijekloFkNavigation)
                    .WithMany(p => p.Matica)
                    .HasForeignKey(d => d.IdPorijekloFk)
                    .HasConstraintName("FK_Matica_Porijeklo");
            });

            modelBuilder.Entity<OkviriZajednice>(entity =>
            {
                entity.HasKey(e => e.IdOkviriZajednice)
                    .HasName("PK_Table_1");

                entity.ToTable("Okviri_Zajednice");

                entity.Property(e => e.IdOkviriZajednice).HasColumnName("ID_Okviri_Zajednice");

                entity.Property(e => e.BrojOkviraZajednice).HasColumnName("Broj_Okvira_Zajednice");

                entity.Property(e => e.DatumOkvira)
                    .HasColumnName("Datum_Okvira")
                    .HasColumnType("datetime");

                entity.Property(e => e.OznakaOkvira)
                    .HasColumnName("Oznaka_Okvira")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.VrstaOkviraFk).HasColumnName("Vrsta_Okvira_FK");

                entity.HasOne(d => d.VrstaOkviraFkNavigation)
                    .WithMany(p => p.OkviriZajednice)
                    .HasForeignKey(d => d.VrstaOkviraFk)
                    .HasConstraintName("FK_Okviri_Zajednice_Vrste_Okvira");
            });

            modelBuilder.Entity<PZajednica>(entity =>
            {
                entity.HasKey(e => e.IdZajednice)
                    .HasName("PK_P.Zajednica");

                entity.ToTable("P.Zajednica");

                entity.Property(e => e.IdZajednice).HasColumnName("ID_Zajednice");

                entity.Property(e => e.IdIzvjescaFk).HasColumnName("ID_Izvjesca_FK");

                entity.Property(e => e.IdMaticeFk).HasColumnName("ID_Matice_FK");

                entity.Property(e => e.IdOkviriZajedniceFk).HasColumnName("ID_Okviri_Zajednice_FK");

                entity.Property(e => e.IdPcelinjakaFk).HasColumnName("ID_Pcelinjaka_FK");

                entity.Property(e => e.IdPorijekloFk).HasColumnName("ID_Porijeklo_FK");

                entity.Property(e => e.IdVrstaKosniceFk).HasColumnName("ID_VrstaKosnice_FK");

                entity.Property(e => e.Opis).HasColumnType("varchar(2000)");

                entity.Property(e => e.OznakaZajednice)
                    .HasColumnName("Oznaka_Zajednice")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdIzvjescaFkNavigation)
                    .WithMany(p => p.PZajednica)
                    .HasForeignKey(d => d.IdIzvjescaFk)
                    .HasConstraintName("FK_P.Zajednica_RadnoIzvjesce");

                entity.HasOne(d => d.IdMaticeFkNavigation)
                    .WithMany(p => p.PZajednica)
                    .HasForeignKey(d => d.IdMaticeFk)
                    .HasConstraintName("FK_P.Zajednica_Matica");

                entity.HasOne(d => d.IdOkviriZajedniceFkNavigation)
                    .WithMany(p => p.PZajednica)
                    .HasForeignKey(d => d.IdOkviriZajedniceFk)
                    .HasConstraintName("FK_P.Zajednica_Okviri_Zajednice");

                entity.HasOne(d => d.IdPcelinjakaFkNavigation)
                    .WithMany(p => p.PZajednica)
                    .HasForeignKey(d => d.IdPcelinjakaFk)
                    .HasConstraintName("FK_P.Zajednica_Pcelinjak");

                entity.HasOne(d => d.IdVrstaKosniceFkNavigation)
                    .WithMany(p => p.PZajednica)
                    .HasForeignKey(d => d.IdVrstaKosniceFk)
                    .HasConstraintName("FK_P.Zajednica_VrstaKosnice");
            });

            modelBuilder.Entity<Pcelar>(entity =>
            {
                entity.HasKey(e => e.IdPcelar)
                    .HasName("PK_Pcelar");

                entity.Property(e => e.IdPcelar).HasColumnName("ID_Pcelar");

                entity.Property(e => e.AccountName).HasColumnType("nchar(15)");

                entity.Property(e => e.AccountPassword).HasColumnType("nchar(15)");

                entity.Property(e => e.ImeiPrezime).HasColumnType("varchar(200)");

                entity.Property(e => e.OznakaPcelar)
                    .HasColumnName("Oznaka_Pcelar")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Pcelinjak>(entity =>
            {
                entity.HasKey(e => e.IdPcelinjak)
                    .HasName("PK_Pcelinjak");

                entity.Property(e => e.IdPcelinjak).HasColumnName("ID_Pcelinjak");

                entity.Property(e => e.IdGeoPolozajFk).HasColumnName("ID_GeoPolozaj_FK");

                entity.Property(e => e.IdPcelaraFkVlasnik).HasColumnName("ID_Pcelara_FK_Vlasnik");

                entity.Property(e => e.Naziv).HasColumnType("varchar(200)");

                entity.Property(e => e.Opis).HasColumnType("varchar(2000)");

                entity.Property(e => e.OznakaPcelinjaka)
                    .HasColumnName("Oznaka_Pcelinjaka")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdGeoPolozajFkNavigation)
                    .WithMany(p => p.Pcelinjak)
                    .HasForeignKey(d => d.IdGeoPolozajFk)
                    .HasConstraintName("FK_Pcelinjak_GeoPolozaj");

                entity.HasOne(d => d.IdPcelaraFkVlasnikNavigation)
                    .WithMany(p => p.Pcelinjak)
                    .HasForeignKey(d => d.IdPcelaraFkVlasnik)
                    .HasConstraintName("FK_Pcelinjak_Pcelar");
            });

            modelBuilder.Entity<Porijeklo>(entity =>
            {
                entity.HasKey(e => e.IdPorijeklo)
                    .HasName("PK_Porijeklo");

                entity.Property(e => e.IdPorijeklo).HasColumnName("ID_Porijeklo");

                entity.Property(e => e.IdVrstaPorijeklaFk).HasColumnName("ID_Vrsta_Porijekla_FK");

                entity.Property(e => e.OznakaPorijekla)
                    .HasColumnName("Oznaka_Porijekla")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdVrstaPorijeklaFkNavigation)
                    .WithMany(p => p.Porijeklo)
                    .HasForeignKey(d => d.IdVrstaPorijeklaFk)
                    .HasConstraintName("FK_Porijeklo_Kategorizacija_stavki_izvjesca");

                entity.HasOne(d => d.IdVrstaPorijeklaFk1)
                    .WithMany(p => p.Porijeklo)
                    .HasForeignKey(d => d.IdVrstaPorijeklaFk)
                    .HasConstraintName("FK_Porijeklo_Vrsta_Porijekla");
            });

            modelBuilder.Entity<RadnoIzvjesce>(entity =>
            {
                entity.HasKey(e => e.IdIzvjesca)
                    .HasName("PK_RadnoIzvjesce");

                entity.Property(e => e.IdIzvjesca).HasColumnName("ID_Izvjesca");

                entity.Property(e => e.IdVrstaIzvjescaFk).HasColumnName("ID_Vrsta_Izvjesca_FK");

                entity.Property(e => e.OznakaIzvjesca)
                    .HasColumnName("Oznaka_Izvjesca")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdVrstaIzvjescaFkNavigation)
                    .WithMany(p => p.RadnoIzvjesce)
                    .HasForeignKey(d => d.IdVrstaIzvjescaFk)
                    .HasConstraintName("FK_RadnoIzvjesce_Vrsta_RIzvjesca");
            });

            modelBuilder.Entity<Radovi>(entity =>
            {
                entity.HasKey(e => e.IdRadovi)
                    .HasName("PK_Radovi");

                entity.Property(e => e.IdRadovi).HasColumnName("ID_Radovi");

                entity.Property(e => e.DatumPocetakRada)
                    .HasColumnName("Datum_Pocetak_Rada")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumZavrsetakRada)
                    .HasColumnName("Datum_Zavrsetak_Rada")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdPcelarFk).HasColumnName("ID_Pcelar_FK");

                entity.Property(e => e.IdPcelinjakFk).HasColumnName("ID_Pcelinjak_FK");

                entity.Property(e => e.IdRadnoIzvjesce).HasColumnName("ID_RadnoIzvjesce");

                entity.Property(e => e.IdZajedniceFk).HasColumnName("ID_Zajednice_FK");

                entity.Property(e => e.OznakaRada)
                    .HasColumnName("Oznaka_Rada")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdPcelarFkNavigation)
                    .WithMany(p => p.Radovi)
                    .HasForeignKey(d => d.IdPcelarFk)
                    .HasConstraintName("FK_Radovi_Pcelar");

                entity.HasOne(d => d.IdPcelinjakFkNavigation)
                    .WithMany(p => p.Radovi)
                    .HasForeignKey(d => d.IdPcelinjakFk)
                    .HasConstraintName("FK_Radovi_Pcelinjak");

                entity.HasOne(d => d.IdRadnoIzvjesceNavigation)
                    .WithMany(p => p.Radovi)
                    .HasForeignKey(d => d.IdRadnoIzvjesce)
                    .HasConstraintName("FK_Radovi_RadnoIzvjesce");

                entity.HasOne(d => d.IdZajedniceFkNavigation)
                    .WithMany(p => p.Radovi)
                    .HasForeignKey(d => d.IdZajedniceFk)
                    .HasConstraintName("FK_Radovi_P.Zajednica");
            });

            modelBuilder.Entity<VrstaKosnice>(entity =>
            {
                entity.HasKey(e => e.IdVrstaKosnice)
                    .HasName("PK_VrstaKosnice");

                entity.Property(e => e.IdVrstaKosnice).HasColumnName("ID_VrstaKosnice");

                entity.Property(e => e.Naziv).HasColumnType("varchar(300)");

                entity.Property(e => e.Opis).HasColumnType("varchar(2000)");

                entity.Property(e => e.OznakaVrsteKosnice)
                    .HasColumnName("Oznaka_Vrste_Kosnice")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<VrstaPorijekla>(entity =>
            {
                entity.HasKey(e => e.IdVrstaPorijekla)
                    .HasName("PK_Vrsta_Porijekla");

                entity.ToTable("Vrsta_Porijekla");

                entity.Property(e => e.IdVrstaPorijekla).HasColumnName("ID_VrstaPorijekla");

                entity.Property(e => e.KlasaPorijekla).HasColumnType("varchar(200)");

                entity.Property(e => e.OpisPorijekla)
                    .HasColumnName("Opis_Porijekla")
                    .HasColumnType("varchar(2000)");
            });

            modelBuilder.Entity<VrstaRizvjesca>(entity =>
            {
                entity.HasKey(e => e.IdVrstaIzvjesca)
                    .HasName("PK_Vrsta_RIzvjesca");

                entity.ToTable("Vrsta_RIzvjesca");

                entity.Property(e => e.IdVrstaIzvjesca).HasColumnName("ID_Vrsta_Izvjesca");

                entity.Property(e => e.IdKategorizacijaStavkiIzvjescaFk).HasColumnName("ID_Kategorizacija_stavki_izvjesca_FK");

                entity.Property(e => e.OpisIzvjesca)
                    .HasColumnName("Opis_Izvjesca")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.TipIzvjesca)
                    .HasColumnName("Tip_Izvjesca")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdKategorizacijaStavkiIzvjescaFkNavigation)
                    .WithMany(p => p.VrstaRizvjesca)
                    .HasForeignKey(d => d.IdKategorizacijaStavkiIzvjescaFk)
                    .HasConstraintName("FK_Vrsta_RIzvjesca_Kategorizacija_stavki_izvjesca");
            });

            modelBuilder.Entity<VrsteOkvira>(entity =>
            {
                entity.HasKey(e => e.IdVrsteOkvira)
                    .HasName("PK_Vrste_Okvira");

                entity.ToTable("Vrste_Okvira");

                entity.Property(e => e.IdVrsteOkvira).HasColumnName("ID_Vrste_okvira");

                entity.Property(e => e.OpisOkvira)
                    .HasColumnName("Opis_Okvira")
                    .HasColumnType("varchar(2000)");
            });
        }
    }
}