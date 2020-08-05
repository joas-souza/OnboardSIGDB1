using FluentValidation;
using OnboardSIGDB1.Dominio.Constantes;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Funcionario : Base<Funcionario>
    { 
        protected Funcionario (){ }

        public Funcionario(int id, string nome, string cpf, DateTime dataContratacao)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataContratacao = dataContratacao;
        }

        public string Nome { get; private set; }

        public string Cpf { get; private set; }

        public DateTime DataContratacao { get; private set; }

        public virtual Empresa Empresa { get; private set; }

        public int? EmpresaId { get; private set; }

        public virtual IList<CargoFuncionario> CargosFuncionario { get; private set; } = new List<CargoFuncionario>();

        //public virtual Cargo Cargo { get; private set; }

        //public int? CargoId { get; private set; }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarDataContratacao(DateTime data)
        {
            DataContratacao = data;
        }

        public void AlterarEmpresa (Empresa empresa)
        {
            Empresa = empresa;
        }

        public void AdicionarCargo(Cargo cargo)
        {
            CargosFuncionario.Add(new CargoFuncionario(cargo,this));
        }

        //public void AlterarCargo(Cargo cargo)
        //{
        //    Cargo = cargo;
        //}

        public override bool Validar()
        {
            RuleFor(f => f.Nome)
            .NotEmpty().WithMessage("Nome inválido")
            .MaximumLength(Consts.QuantidadeMaximaDeCaracteresParaNome).WithMessage("O nome deve ter no máximo 150 caracteres");

            RuleFor(f => f.Cpf)
            .MaximumLength(Consts.TamanhoMaximoCnpj)
            .Must(CpfValido).WithMessage("Cpf inválido");

            RuleFor(f => f.DataContratacao.ToString())
                .Must(DataValida).WithMessage("Data da contratação inválida");

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

        private static bool CpfValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
