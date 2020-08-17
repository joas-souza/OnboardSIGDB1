using Bogus;
using Bogus.Extensions.Brazil;
using ExpectedObjects;
using System;
using Xunit;

namespace OnboardSIGDB1.DominioTeste.Funcionario
{
    public class FuncionarioTeste
    {
        private readonly int _id;
        private readonly string _nome;
        private readonly string _cpf;
        private readonly DateTime _dataContratacao;

        private readonly Faker _faker;

        public FuncionarioTeste()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
            _cpf = _faker.Person.Cpf().Replace(".","").Replace("-","");
            _dataContratacao = DateTime.Now;
        }

        [Fact]
        public void DeveCriarFuncionario()
        {
            var funcionarioEsperado = new
            {
                Id = _id,
                Nome = _nome,
                Cpf = _cpf,
                DataContratacao = _dataContratacao
            };

            var funcionario = new Dominio.Entidades.Funcionario(funcionarioEsperado.Nome, funcionarioEsperado.Cpf, funcionarioEsperado.DataContratacao);

            funcionarioEsperado.ToExpectedObject().ShouldMatch(funcionario);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveTerNomeInvalido(string nomeInvalido)
        {
            var funcionario = FuncionarioBuilder.Novo().ComNome(nomeInvalido).Build();

            Assert.False(funcionario.Validar());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("000.111.222.4")]
        [InlineData("000.111.333-4")]
        [InlineData("0001115554")]
        [InlineData("000")]
        public void NaoDeveTerCpfInvalido(string cpfInvalido)
        {
            var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();

            Assert.False(funcionario.Validar());
        }

        [Fact]
        public void NaoDeveTerDataInvalida()
        {
            var dataInvalida = DateTime.MinValue;
            var funcionario = FuncionarioBuilder.Novo().ComDataContratacao(dataInvalida).Build();

            Assert.False(funcionario.Validar());
        }


        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;
            var funcionario = FuncionarioBuilder.Novo().Build();

            funcionario.AlterarNome(nomeEsperado);

            Assert.True(funcionario.Validar());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var funcionario = FuncionarioBuilder.Novo().Build();
            funcionario.AlterarNome(nomeInvalido);

            Assert.False(funcionario.Validar());
        }

        [Fact]
        public void DeveAlterarData()
        {
            var dataEsperada = DateTime.Now;
            var funcionario = FuncionarioBuilder.Novo().Build();
            
            funcionario.AlterarDataContratacao(dataEsperada);

            Assert.True(funcionario.Validar());
        }

        [Fact]
        public void NaoDeveAlterarDataInvalida()
        {
            var dataInvalida = DateTime.MinValue;
            var funcionario = FuncionarioBuilder.Novo().Build();

            funcionario.AlterarDataContratacao(dataInvalida);

            Assert.False(funcionario.Validar());
        } 
    }
}
