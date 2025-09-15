using alugueis_api.Models;
using Microsoft.EntityFrameworkCore;

namespace alugueis_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Locatario> Locatarios { get; set; }
        public DbSet<Apto> Aptos { get; set; }
        public DbSet<Predio> Predios { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<TipoDespesa> TiposDespesa { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaRateio> DespesaRateios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locatario>()
                .HasKey(l => l.Cpf );

            modelBuilder.Entity<Apto>()
                .HasKey(a => a.CodApto);

            modelBuilder.Entity<Apto>()
                .HasOne(a => a.Predio)
                .WithMany(p => p.Aptos)
                .HasForeignKey(a => a.CodPredio);

            modelBuilder.Entity<Predio>()
                .HasKey(p => p.CodPredio);

            modelBuilder.Entity<Locacao>()
                .HasKey(l => l.CodLocacao);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Locatario)
                .WithMany(loc => loc.Locacoes)
                .HasForeignKey(l => l.Cpf);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Apto)
                .WithMany(a => a.Locacoes)
                .HasForeignKey(l => l.CodApto);

            modelBuilder.Entity<TipoDespesa>()
                .HasKey(t => t.CodTipo);

            modelBuilder.Entity<Despesa>()
                .HasKey(d => d.CodDespesa);

            modelBuilder.Entity<Despesa>()
                .HasOne(d => d.TipoDespesa)
                .WithMany(td => td.Despesas)
                .HasForeignKey(d => d.CodTipoDespesa);

            modelBuilder.Entity<DespesaRateio>()
                .HasOne(dr => dr.Despesa)
                .WithMany(d => d.Rateios)
                .HasForeignKey(dr => dr.CodDespesa);

            modelBuilder.Entity<DespesaRateio>()
                .HasOne(dr => dr.Apto)
                .WithMany(a => a.DespesasRateio)
                .HasForeignKey(dr => dr.CodApto);

            modelBuilder.Entity<DespesaRateio>()
                .HasKey(dr => new { dr.CodDespesa, dr.CodApto });
        }
    }
}
