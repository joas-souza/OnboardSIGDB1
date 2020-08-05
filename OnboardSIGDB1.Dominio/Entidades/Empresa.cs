using FluentValidation;
using OnboardSIGDB1.Dominio.Constantes;
using System;
using System.Globalization;

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
            .NotEmpty().WithMessage("Nome inválido")
            .MaximumLength(Consts.QuantidadeMaximaDeCaracteresParaNome).WithMessage("O nome deve ter no máximo 150 caracteres");

            RuleFor(e => e.Cnpj)
            .MaximumLength(Consts.TamanhoMaximoCnpj)
            .Must(CnpjValido).WithMessage("Cnpj inválido");
 
            RuleFor(e => e.DataFundacao.ToString())
                .Must(DataValida).WithMessage("Data da fundação inválida");

            Result = Validate(this);

            return Validate(this).IsValid;
        }

        private static bool DataValida(string data)
        {
            if (DateTime.TryParse(data, out var resultado))
                    return true;
            else
                return false;
        }

        private static bool CnpjValido(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
