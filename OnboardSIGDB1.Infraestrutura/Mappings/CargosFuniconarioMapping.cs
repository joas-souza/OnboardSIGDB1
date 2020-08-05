using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1.Dominio.Entidades;


namespace OnboardSIGDB1.Infraestrutura.Mappings
{
    public class CargosFuniconarioMapping : IEntityTypeConfiguration<CargoFuncionario>
    {
        public void Configure(EntityTypeBuilder<CargoFuncionario> builder)
        {
            builder.HasKey(p => new
            {
                p.CargoId,
                p.FuncionarioId
            });

            builder.Property(p => p.CargoId).IsRequired();
            builder.Property(p => p.FuncionarioId).IsRequired();

            builder.HasOne(bc => bc.Funcionario)
                .WithMany("CargosFuncionario")
                 .HasForeignKey(bc => bc.FuncionarioId);

            builder.HasOne(bc => bc.Cargo);
        }
    }
}
