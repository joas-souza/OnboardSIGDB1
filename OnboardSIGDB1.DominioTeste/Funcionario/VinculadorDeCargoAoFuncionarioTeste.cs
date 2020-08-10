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
    public class VinculadorDeCargoAoFuncionarioTeste
    {
        private readonly FuncionarioDto _funcionarioDto;
        private readonly VinculadorDeCargoAoFuncionario _vinculadorDeCargoAoFuncionario;
        private readonly Mock<IRepositorioDeFuncionario> _funcionarioRepositorioMock;
        private readonly Mock<IRepositorioDeCargo> _repositorioDeCargo;
        private readonly Mock<NotificationContext> _notificationContext;
        private readonly Faker _fake;

        public VinculadorDeCargoAoFuncionarioTeste()
        {
            _fake = new Faker();
            _funcionarioDto = new FuncionarioDto
            {
                Nome = _fake.Person.FullName,
                Cpf = _fake.Person.Cpf(),
                DataContratacao = DateTime.Now,
                EmpresaId = null,
                CargoId = 1
            };
            _funcionarioRepositorioMock = new Mock<IRepositorioDeFuncionario>();
            _repositorioDeCargo = new Mock<IRepositorioDeCargo>();
            _notificationContext = new Mock<NotificationContext>();
            _vinculadorDeCargoAoFuncionario = new VinculadorDeCargoAoFuncionario(_funcionarioRepositorioMock.Object, _repositorioDeCargo.Object, _notificationContext.Object);
        }

        [Fact]
        public async Task NaoDeveVincularCargoAFuncionarioNaoExistente()
        {
            _funcionarioDto.Id = 1;
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id));

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Contains(Resource.FuncionarioNaoLocalizado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }

        [Fact]
        public async Task NaoDeveVincularCargoAFuncionarioSemEmpresa()
        {
            _funcionarioDto.Id = 1;
            var funcionario = FuncionarioBuilder.Novo().Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Contains(Resource.FuncionarioNaoVinculadoEmpresa, _notificationContext.Object.Notifications.Select(n => n.Message));
        }

        [Fact]
        public async Task DeveVincularCargoAoFuncionario()
        {
            _funcionarioDto.Id = 1;
            _funcionarioDto.CargoId = 1;
            var funcionario = FuncionarioBuilder.Novo().ComEmpresa(1).Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);
            _repositorioDeCargo.Setup(c => c.RecuperarPorId((int)_funcionarioDto.CargoId)).ReturnsAsync(new Cargo(1, "Desenvolvedor"));

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Equal(_funcionarioDto.CargoId, funcionario.CargosFuncionario.FirstOrDefault().Cargo.Id);
        }

        [Fact]
        public async Task NaoDeveVincularCargoNaoExistente()
        {
            _funcionarioDto.Id = 1;
            _funcionarioDto.CargoId = 1;
            var funcionario = FuncionarioBuilder.Novo().ComEmpresa(1).Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Contains(Resource.CargoNaoLocalizado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }
    }
}
