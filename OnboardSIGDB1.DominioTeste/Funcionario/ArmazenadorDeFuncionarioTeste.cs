using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils;
using OnboardSIGDB1.Utils.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnboardSIGDB1.DominioTeste.Funcionario
{
    public class ArmazenadorDeFuncionarioTeste
    {
        private readonly FuncionarioDto _funcionarioDto;
        private readonly ArmazenadorDeFuncionario _armazenadorDeFuncionario;
        private readonly Mock<IRepositorioDeFuncionario> _funcionarioRepositorioMock;
        private readonly Mock<NotificationContext> _notificationContext;
        private readonly Mock<IConsultasDeFuncionario> _consultasDeFuncionario;

        public ArmazenadorDeFuncionarioTeste()
        {
            var fake = new Faker();
            _funcionarioDto = new FuncionarioDto
            {
                Id = 34,
                Nome = fake.Person.FullName,
                Cpf = Util.RemoverMascara(fake.Person.Cpf()),
                DataContratacao = DateTime.Now,
                EmpresaId = null,
                CargoId = null
            };
            _funcionarioRepositorioMock = new Mock<IRepositorioDeFuncionario>();
            _consultasDeFuncionario = new Mock<IConsultasDeFuncionario>();
            _notificationContext = new Mock<NotificationContext>();
            _armazenadorDeFuncionario = new ArmazenadorDeFuncionario(_funcionarioRepositorioMock.Object, _consultasDeFuncionario.Object, _notificationContext.Object);
        }

        [Fact]
        public async Task DeveAdicionarFuncionario()
        {
            await _armazenadorDeFuncionario.Salvar(_funcionarioDto);

            _funcionarioRepositorioMock.Verify(r => r.Salvar(It.Is<Dominio.Entidades.Funcionario>(f => f.Nome == _funcionarioDto.Nome && f.Cpf == _funcionarioDto.Cpf)));
        }

        [Fact]
        public async Task NaoDeveAdicionarFuncionarioComMesmoCpfDeOutroJaSalvo()
        {
            var funcionarioJaSalvo = FuncionarioBuilder.Novo().ComId(_funcionarioDto.Id).ComCpf(_funcionarioDto.Cpf).Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorCpf(_funcionarioDto.Cpf)).ReturnsAsync(funcionarioJaSalvo);

            await _armazenadorDeFuncionario.Salvar(_funcionarioDto);

            //_notificationContext.Verify(n => It.Is<NotificationContext>(f => f.Notifications.Where(m => m.Message.Equals(Resource.FuncionarioJaCadastrado))));

            Assert.Contains(Resource.FuncionarioJaCadastrado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }

        [Fact]
        public async Task NaoDeveAdicionarFuncionarioQuandoTiverNotificacaoDeErro()
        {
            _funcionarioDto.Cpf = string.Empty;

            await _armazenadorDeFuncionario.Salvar(_funcionarioDto);

            /*Exemplo: _notificadorDeDominioMock.Verify(_ => _.HandleAsync(It.IsAny<DomainNotification>()), Times.Once);*/
            //_notificationContext.Verify(_ => (It.IsAny<NotificationContext>()), Times.Once);

            Assert.True(_notificationContext.Object.HasNotifications);
        }
    }
}
