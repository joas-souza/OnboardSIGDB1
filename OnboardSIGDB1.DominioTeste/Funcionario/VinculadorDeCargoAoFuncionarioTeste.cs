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
        private readonly Cargo _cargo;

        public VinculadorDeCargoAoFuncionarioTeste()
        {
            _fake = new Faker();
            _cargo = new Cargo(1, "Desenvolvedor");
            _funcionarioDto = new FuncionarioDto
            {
                Id = 1,
                Nome = _fake.Person.FullName,
                Cpf = _fake.Person.Cpf(),
                DataContratacao = DateTime.Now,
                EmpresaId = 1,
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
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id));

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Contains(Resource.FuncionarioNaoLocalizado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }

        [Fact]
        public async Task NaoDeveVincularCargoAFuncionarioSemEmpresa()
        {
            var funcionario = FuncionarioBuilder.Novo().Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Contains(Resource.FuncionarioNaoVinculadoEmpresa, _notificationContext.Object.Notifications.Select(n => n.Message));
        }

        [Fact]
        public async Task DeveVincularCargoAoFuncionario()
        {
            var funcionario = FuncionarioBuilder.Novo().ComEmpresa((int)_funcionarioDto.EmpresaId).Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);
            _repositorioDeCargo.Setup(c => c.RecuperarPorId((int)_funcionarioDto.CargoId)).ReturnsAsync(_cargo);

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Equal(_funcionarioDto.CargoId, funcionario.CargosFuncionario.FirstOrDefault().Cargo.Id);
        }

        [Fact]
        public async Task NaoDeveVincularCargoNaoExistente()
        {
            var funcionario = FuncionarioBuilder.Novo().ComEmpresa((int)_funcionarioDto.EmpresaId).Build();
            _funcionarioRepositorioMock.Setup(r => r.RecuperarPorId(_funcionarioDto.Id)).ReturnsAsync(funcionario);

            await _vinculadorDeCargoAoFuncionario.VincularCargo(_funcionarioDto.Id, (int)_funcionarioDto.CargoId);

            Assert.Contains(Resource.CargoNaoLocalizado, _notificationContext.Object.Notifications.Select(n => n.Message));
        }
    }
}
