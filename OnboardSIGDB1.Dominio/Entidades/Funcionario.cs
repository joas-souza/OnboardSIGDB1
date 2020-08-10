using FluentValidation;
using OnboardSIGDB1.Utils;
using OnboardSIGDB1.Utils.Constantes;
using OnboardSIGDB1.Utils.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Dominio.Entidades
{
    public class Funcionario : Base<Funcionario>
    { 
        protected Funcionario (){ }

        public Funcionario(string nome, string cpf, DateTime dataContratacao)
        {
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

        public void AlterarEmpresaId(int id)
        {
            EmpresaId = id;
        }

        public void AlterarEmpresa (Empresa empresa)
        {
            Empresa = empresa;
        }

        public void AdicionarCargo(Cargo cargo)
        {
            CargosFuncionario.Add(new CargoFuncionario(cargo,this));
        }

        public void RemoveCargo(Cargo cargo)
        {
            var cargoFuncionario = CargosFuncionario.FirstOrDefault(c => c.CargoId == cargo.Id);

            CargosFuncionario.Remove(cargoFuncionario);
        }

        public void ExcluirCargo()
        {
            CargosFuncionario = null;
        }

        //public void AlterarCargo(Cargo cargo)
        //{
        //    Cargo = cargo;
        //}

        public override bool Validar()
        {
            RuleFor(f => f.Nome)
            .NotEmpty().WithMessage(Resource.NomeInvalido)
            .MaximumLength(Consts.QuantidadeMaximaDeCaracteresParaNome).WithMessage(Resource.TamanhoMaximoDoNome);

            RuleFor(f => f.Cpf)
            .MaximumLength(Consts.TamanhoMaximoCpf)
            .Must(Util.CpfValido).WithMessage(Resource.CpfInvalido);

            RuleFor(f => f.DataContratacao)
                .Must(Util.DataValida).WithMessage(Resource.DataInvalida);

            Result = Validate(this);

            return Validate(this).IsValid;
        }
    }
}
