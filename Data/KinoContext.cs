using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Data
{
    public class KinoContext : DbContext
    {
        public KinoContext (DbContextOptions<KinoContext> options)
            : base(options)
        {
        }

        public DbSet<webapp.Models.Bilety> Bilety { get; set; }
        public DbSet<webapp.Models.Klienci> Klienci { get; set; }
        public DbSet<webapp.Models.Aktorzy> Aktorzy { get; set; }
        public DbSet<webapp.Models.Filmy> Filmy { get; set; }
        public DbSet<webapp.Models.Gatunki> Gatunki { get; set; }
        public DbSet<webapp.Models.Pracownicy> Pracownicy { get; set; }
        public DbSet<webapp.Models.Sale> Sale { get; set; }
        public DbSet<webapp.Models.Seanse> Seanse { get; set; }
        public DbSet<webapp.Models.Pracownicy_seanse> Pracownicy_Seanse { get; set; }
        public DbSet<webapp.Models.Aktorzy_filmy> Aktorzy_filmy { get; set; }
        public DbSet<webapp.Models.Gatunki_filmy> Gatunki_filmy { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bilety>().ToTable("Bilety");
            modelBuilder.Entity<Klienci>().ToTable("Klienci");
            modelBuilder.Entity<Aktorzy>().ToTable("Aktorzy");
            modelBuilder.Entity<Filmy>().ToTable("Filmy");
            modelBuilder.Entity<Gatunki>().ToTable("Gatunki");
            modelBuilder.Entity<Pracownicy>().ToTable("Pracownicy");
            modelBuilder.Entity<Sale>().ToTable("Sale");
            modelBuilder.Entity<Seanse>().ToTable("Seanse");
            modelBuilder.Entity<Pracownicy_seanse>().HasKey(i => new {i.PracownicyId, i.SeanseId});
            modelBuilder.Entity<Gatunki_filmy>().HasKey(i => new { i.GatunkiId, i.FilmyId});
            modelBuilder.Entity<Aktorzy_filmy>().HasKey(i => new { i.AktorzyId, i.FilmyId });

        }

    }
}
