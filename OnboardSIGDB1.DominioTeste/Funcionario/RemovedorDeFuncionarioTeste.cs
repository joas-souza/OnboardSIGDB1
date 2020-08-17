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
    public class RemovedorDeFuncionarioTeste
    {
        private readonly FuncionarioDto _funcionarioDto;
        private readonly RemovedorDeFuncionario _removedorDeFuncionario;
        private readonly Mock<IRepositorioDeFuncionario> _funcionarioRepositorioMock;
        private readonly Mock<NotificationContext> _notificationContext;
        private readonly Faker _fake;

        public RemovedorDeFuncionarioTeste()
        {
            _fake = new Faker();
            _funcionarioDto = new FuncionarioDto
            {
                Id = 34,
                Nome = _fake.Person.FullName,
                Cpf = _fake.Person.Cpf(),
                DataContratacao = DateTime.Now,
                EmpresaId = 34,
                CargoId = 1
            };
            _funcionarioRepositorioMock = new Mock<IRepositorioDeFuncionario>();
            _notificationContext = new Mock<NotificationContext>();
            _removedorDeFuncionario = new RemovedorDeFuncionario(_funcionarioRepositorioMock.Object, _notificationContext.Object);
        }

        [Fact]
        public async Task DeveRemoverFuncionario()
        {
            var funcionario = FuncionarioBuilder.Novo().ComEmpresa((int)_funcionarioDto.EmpresaId).Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _removedorDeFuncionario.Excluir(_funcionarioDto.Id);

            _funcionarioRepositorioMock.Verify(r => r.Excluir(It.IsAny<int>()));
        }

        [Fact]
        public async Task NaoRemoverFuncionarioNaoExistente()
        {
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id));

            await _removedorDeFuncionario.Excluir(_funcionarioDto.Id);

            Assert.Contains(Resource.FuncionarioNaoLocalizado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }
    }
}
