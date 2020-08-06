using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class VinculadorDeCargoAoFuncionario : IVinculadorDeCargoAoFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;
        private readonly IRepositorioDeCargo _repositorioDeCargo;
        private readonly NotificationContext _notificationContext;


        public VinculadorDeCargoAoFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario, 
            IRepositorioDeCargo repositorioDeCargo, 
            NotificationContext notificationContext)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
            _repositorioDeCargo = repositorioDeCargo;
            _notificationContext = notificationContext;
        }

        public async Task VincularCargo(int id, int cargoId)
        {
            var funcionario = await _repositorioDeFuncionario.RecuperarPorId(id);

            ValidarFuncionario(funcionario);
            ValidarEmpresa(funcionario.EmpresaId);

            if (!_notificationContext.HasNotifications)
            {
                var cargo = await _repositorioDeCargo.RecuperarPorId(cargoId);

                if (cargo != null)
                {
                    funcionario.AdicionarCargo(cargo);

                    //funcionario.AlterarCargo(cargo);
                   // await _repositorioDeFuncionario.Alterar(funcionario);
                }
                else
                    _notificationContext.AddNotification("", Resource.CargoNaoLocalizado);
            }
        }

        private void ValidarFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
                _notificationContext.AddNotification("", Resource.FuncionarioNaoLocalizado);
        }

        private void ValidarEmpresa(int? empresaId)
        {
            if (empresaId  == null)
                _notificationContext.AddNotification("", Resource.FuncionarioNaoVinculadoEmpresa);
        }
    }
}
