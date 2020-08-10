using FluentValidation;
using OnboardSIGDB1.Utils;
using OnboardSIGDB1.Utils.Constantes;
using OnboardSIGDB1.Utils.Resources;
using System;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Empresa : Base<Empresa>
    {
        protected Empresa() { }
        
        public Empresa(int id, string nome, string cnpj, DateTime dataFundacao)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
        }

        public string Nome { get; private set; }

        public string Cnpj { get; private set; }

        public DateTime DataFundacao { get; private set; }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarDataFundacao(DateTime data)
        {
            DataFundacao = data;
        }

        public override bool Validar()
        {
            RuleFor(e => e.Nome)
            .NotEmpty().WithMessage(Resource.NomeInvalido)
            .MaximumLength(Consts.QuantidadeMaximaDeCaracteresParaNome).WithMessage(Resource.TamanhoMaximoDoNome);

            RuleFor(e => e.Cnpj)
            .MaximumLength(Consts.TamanhoMaximoCnpj)
            .Must(Util.CnpjValido).WithMessage(Resource.CnpjInvalido);
 
            RuleFor(e => e.DataFundacao)
                .Must(Util.DataValida).WithMessage(Resource.DataInvalida);

            Result = Validate(this);

            return Validate(this).IsValid;
        }
    }
}
