using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1.Dominio.Constantes;
using OnboardSIGDB1.Dominio.Entidades;


namespace OnboardSIGDB1.Infraestrutura.Mappings
{
    public class CargoMapping : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.Property(p => p.Descricao)
                .HasMaxLength(Consts.QuantidadeMaximaDeCaracteresParaDescricao)
                .IsRequired();

            builder.Ignore(i => i.CargosFuncionario);
            builder.Ignore(i => i.CascadeMode);
            builder.Ignore(i => i.Result);
        }
    }
}
