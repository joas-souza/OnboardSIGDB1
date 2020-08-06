using FluentValidation;
using OnboardSIGDB1.Utils.Constantes;
using OnboardSIGDB1.Utils.Resources;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Cargo : Base<Cargo>
    {
        protected Cargo() { }

        public Cargo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public string Descricao { get; set; }

        public virtual IList<CargoFuncionario> CargosFuncionario { get; private set; }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public override bool Validar()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage(Resource.DescricaoInvalida)
                .MaximumLength(Consts.QuantidadeMaximaDeCaracteresParaDescricao).WithMessage(Resource.TamanhoMaximoDaDescricao);

            Result = Validate(this);

            return Validate(this).IsValid;
        }
    }
}
