using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System;
using System.Linq;
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
        private readonly int _idFuncionarioExcluir;

        public AlteradorDeFuncionarioTeste()
        {
            _fake = new Faker();
            _idFuncionarioExcluir = 4;
            _funcionarioDto = new FuncionarioDto
            {
                Id = 1,
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
            await _alteradorDeFuncionario.Alterar(_idFuncionarioExcluir, _funcionarioDto);

            Assert.Contains(Resource.FuncionarioNaoIdentificado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }
    }
}
