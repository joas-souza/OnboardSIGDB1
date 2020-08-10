using Bogus;
using Bogus.Extensions.Brazil;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Utils;
using System;
using System.Collections.Generic;

namespace OnboardSIGDB1.DominioTeste.Funcionario
{
    public class FuncionarioBuilder
    {
        protected int Id;
        protected string Nome;
        protected string Cpf;
        protected DateTime DataContratacao;
        protected int? EmpresaId;
        protected IList<CargoFuncionario> CargosFuncionario;

        public static FuncionarioBuilder Novo()
        {
            var fake = new Faker();

            return new FuncionarioBuilder {
                Nome = fake.Person.FullName,
                Cpf = Util.RemoverMascara(fake.Person.Cpf()),
                DataContratacao = DateTime.Now,
                EmpresaId = null,
                CargosFuncionario = null
            };

        }

        public FuncionarioBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public FuncionarioBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public FuncionarioBuilder ComCpf(string cpf)
        {
            Cpf  = cpf;
            return this;
        }

        public FuncionarioBuilder ComDataContratacao(DateTime data)
        {
            DataContratacao = data;
            return this;
        }

        public FuncionarioBuilder ComEmpresa(int id)
        {
            EmpresaId = id;
            return this;
        }

        public FuncionarioBuilder ComCargo(Cargo cargo)
        {
            CargosFuncionario.Add(new CargoFuncionario(cargo, Build()));
            return this;
        }

        public Dominio.Entidades.Funcionario Build()
        {
            var funcionario =  new Dominio.Entidades.Funcionario(Nome, Cpf, DataContratacao);
            if(EmpresaId>0)
                funcionario.AlterarEmpresaId(1);

            if (Id <= 0) return funcionario;

            var propertyInfo = funcionario.GetType().GetProperty("Id");
            propertyInfo.SetValue(funcionario, Convert.ChangeType(Id, propertyInfo.PropertyType), null);

            return funcionario;
        }
    }
}
