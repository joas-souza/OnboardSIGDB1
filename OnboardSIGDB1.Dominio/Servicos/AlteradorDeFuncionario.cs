using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class AlteradorDeFuncionario : IAlteradorDeFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;
        private readonly NotificationContext _notificationContext;

        public AlteradorDeFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario,
             NotificationContext notificationContext)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
            _notificationContext = notificationContext;
        }

        public async Task Alterar(int id, FuncionarioDto funcionarioDto)
        {
            if (ValidarId(id, funcionarioDto))
            {
                var funcionario = await _repositorioDeFuncionario.RecuperarPorId(id);

                if (funcionario != null)
                {
                    funcionario.AlterarNome(funcionarioDto.Nome);
                    funcionario.AlterarDataContratacao(funcionarioDto.DataContratacao);

                    if (funcionario.Validar())
                        await _repositorioDeFuncionario.Alterar(funcionario);
                    else
                        _notificationContext.AddNotifications(funcionario.Result);
                }
                else
                    _notificationContext.AddNotification("", "Funcionário não localizado");
            }
        }

        private bool ValidarId(int id, FuncionarioDto funcionarioDto)
        {
            if (id != funcionarioDto.Id)
            {
                _notificationContext.AddNotification("", "Funcionário não identificado");
                return false;
            }

            return true;
        }
    }
}
