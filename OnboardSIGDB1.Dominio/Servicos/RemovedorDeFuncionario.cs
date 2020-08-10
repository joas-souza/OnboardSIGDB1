using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class RemovedorDeFuncionario : IRemovedorDeFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;
        private readonly NotificationContext _notificationContext;

        public RemovedorDeFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario, NotificationContext notificationContext)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
            _notificationContext = notificationContext;
        }

        public async Task Excluir(int id)
        {
            var funcionario = await _repositorioDeFuncionario.RecuperarPorId(id);

            if(funcionario!=null)
            {
                funcionario.ExcluirCargo();
                await _repositorioDeFuncionario.Excluir(id);
            }
            else
                _notificationContext.AddNotification("", Resource.FuncionarioNaoLocalizado);
        }
    }
}
