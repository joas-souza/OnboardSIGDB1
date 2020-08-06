using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Utils.Constantes;

namespace OnboardSIGDB1.Infraestrutura.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Property(p => p.Nome)
                .HasMaxLength(Consts.QuantidadeMaximaDeCaracteresParaNome)
                .IsRequired();

            builder.Property(p => p.Cnpj)
                  .HasMaxLength(Consts.TamanhoMaximoCnpj)
                  .IsRequired();

            builder.Ignore(i => i.CascadeMode);
            builder.Ignore(i => i.Result);
        }
    }
}
