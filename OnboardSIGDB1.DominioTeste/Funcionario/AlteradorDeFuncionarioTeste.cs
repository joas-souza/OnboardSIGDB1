using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnboardSIGDB1.DominioTeste.Funcionario
{
    public class AlteradorDeFuncionarioTeste
    {
        private readonly FuncionarioDto _funcionarioDto;
        private readonly AlteradorDeFuncionario _alteradorDeFuncionario;
        private readonly Mock<IRepositorioDeFuncionario> _funcionarioRepositorioMock;
        private readonly Mock<NotificationContext> _notificationContext;
        private readonly Faker _fake;

        public AlteradorDeFuncionarioTeste()
        {
            _fake = new Faker();
            _funcionarioDto = new FuncionarioDto
            {
                Nome = _fake.Person.FullName,
                Cpf = _fake.Person.Cpf(),
                DataContratacao = DateTime.Now,
                EmpresaId = null,
                CargoId = null
            };
            _funcionarioRepositorioMock = new Mock<IRepositorioDeFuncionario>();
            _notificationContext = new Mock<NotificationContext>();
            _alteradorDeFuncionario = new AlteradorDeFuncionario(_funcionarioRepositorioMock.Object, _notificationContext.Object);
        }

        [Fact]
        public async Task DeveAlterarDadosDoFuncionario()
        {
            _funcionarioDto.Id = 1;
            _funcionarioDto.Nome = _fake.Person.FullName;
            _funcionarioDto.DataContratacao = DateTime.Now;
            var funcionario = FuncionarioBuilder.Novo().Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _alteradorDeFuncionario.Alterar(_funcionarioDto.Id, _funcionarioDto);

            Assert.Equal(_funcionarioDto.Nome, funcionario.Nome);
            Assert.Equal(_funcionarioDto.DataContratacao, funcionario.DataContratacao);
        }

        [Fact]
        public async Task NaoDeveAlterarFuncionarioComIdDivergente()
        {
            _funcionarioDto.Id = 4;

            await _alteradorDeFuncionario.Alterar(5, _funcionarioDto);

            Assert.Contains(Resource.FuncionarioNaoIdentificado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }
    }
}
