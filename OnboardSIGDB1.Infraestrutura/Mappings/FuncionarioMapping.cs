using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Utils.Constantes;

namespace OnboardSIGDB1.Infraestrutura.Mappings
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.Property(p => p.Nome)
                .HasMaxLength(Consts.QuantidadeMaximaDeCaracteresParaNome)
                .IsRequired();

            builder.Property(p => p.Cpf)
               .HasMaxLength(Consts.TamanhoMaximoCpf)
               .IsRequired();

            builder.Property(p => p.EmpresaId).IsRequired(false);

            builder.HasOne(p => p.Empresa)
                .WithMany()
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Property(p => p.CargoId).IsRequired(false);

            //builder.HasOne(p => p.Cargo)
            //    .WithMany()
            //    .HasForeignKey(p => p.CargoId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(i => i.CargosFuncionario);
            builder.Ignore(i => i.CascadeMode);
            builder.Ignore(i => i.Result);
        }
    }
}
