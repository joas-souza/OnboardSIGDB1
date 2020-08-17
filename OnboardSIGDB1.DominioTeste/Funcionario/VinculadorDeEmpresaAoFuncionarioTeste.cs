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
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnboardSIGDB1.DominioTeste.Funcionario
{
    public class VinculadorDeEmpresaAoFuncionarioTeste
    {
        private readonly FuncionarioDto _funcionarioDto;
        private readonly VinculadorDeEmpresaAoFuncionario _vinculadorDeEmpresaAoFuncionario;
        private readonly Mock<IRepositorioDeFuncionario> _funcionarioRepositorioMock;
        private readonly Mock<IRepositorioDeEmpresa> _repositorioDeEmpresa;
        private readonly Mock<NotificationContext> _notificationContext;
        private readonly Empresa _empresa;
        private readonly Faker _fake;

        public VinculadorDeEmpresaAoFuncionarioTeste()
        {
            _fake = new Faker();
            _funcionarioDto = new FuncionarioDto
            {
                Id = 1,
                Nome = _fake.Person.FullName,
                Cpf = _fake.Person.Cpf(),
                DataContratacao = DateTime.Now,
                EmpresaId = 2,
                CargoId = 1
            };
            _funcionarioRepositorioMock = new Mock<IRepositorioDeFuncionario>();
            _repositorioDeEmpresa = new Mock<IRepositorioDeEmpresa>();
            _notificationContext = new Mock<NotificationContext>();
            _vinculadorDeEmpresaAoFuncionario = new VinculadorDeEmpresaAoFuncionario(_funcionarioRepositorioMock.Object, _repositorioDeEmpresa.Object, _notificationContext.Object);
            _empresa = new Empresa(2, "DB1", "001.002.455/0001-75", DateTime.Now);
        }

        [Fact]
        public async Task NaoDeveVincularEmpresaAFuncionarioNaoExistente()
        {
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id));

            await _vinculadorDeEmpresaAoFuncionario.VincularEmpresa(_funcionarioDto.Id, (int)_funcionarioDto.EmpresaId);

            Assert.Contains(Resource.FuncionarioNaoLocalizado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }

        [Fact]
        public async Task DeveVincularEmpresaAoFuncionario()
        {
            var funcionario = FuncionarioBuilder.Novo().Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);
            _repositorioDeEmpresa.Setup(e => e.RecuperarPorId((int)_funcionarioDto.EmpresaId)).ReturnsAsync(_empresa);

            await _vinculadorDeEmpresaAoFuncionario.VincularEmpresa(_funcionarioDto.Id, (int)_funcionarioDto.EmpresaId);

            Assert.Equal(_funcionarioDto.EmpresaId, funcionario.Empresa.Id);
        }

        [Fact]
        public async Task NaoDeveVincularEmpresaNaoExistente()
        {
            var funcionario = FuncionarioBuilder.Novo().Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _vinculadorDeEmpresaAoFuncionario.VincularEmpresa(_funcionarioDto.Id, (int)_funcionarioDto.EmpresaId);

            Assert.Contains(Resource.EmpresaNaoLocalizada, _notificationContext.Object.Notifications.Select(n => n.Message));
        }
    }
}
