using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
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
                    _notificationContext.AddNotification("", "Cargo não localizado");
            }
        }

        private void ValidarFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
                _notificationContext.AddNotification("", "Funcionário não localizado");
        }

        private void ValidarEmpresa(int? empresaId)
        {
            if (empresaId  == null)
                _notificationContext.AddNotification("", "Funcionário não vinculado a nenhuma empresa");
        }
    }
}
