using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Infraestrutura.Mappings;

namespace OnboardSIGDB1.Infraestrutura.Contexto
{
    public class OnboardDbContext : DbContext
    {

        public OnboardDbContext(DbContextOptions<OnboardDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<CargoFuncionario> CargosFuncionario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            modelBuilder.ApplyConfiguration(new CargoMapping());
            modelBuilder.ApplyConfiguration(new CargosFuncionarioMapping());
        }
    }
}
