using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class AlteradorDeEmpresa : IAlteradorDeEmpresa
    {
        private readonly IRepositorioDeEmpresa _repositorioDeEmpresa;
        private readonly NotificationContext _notificationContext;

        public AlteradorDeEmpresa(IRepositorioDeEmpresa repositorioDeEmpresa,
            NotificationContext notificationContext)
        {
            _repositorioDeEmpresa = repositorioDeEmpresa;
            _notificationContext = notificationContext;
        }

       
        public async Task  Alterar(int id, EmpresaDto empresaDto)
        {
            if (ValidarId(id, empresaDto))
            {
                var empresa = await _repositorioDeEmpresa.RecuperarPorId(id);

                if (empresa != null)
                {
                    empresa.AlterarNome(empresaDto.Nome);
                    empresa.AlterarDataFundacao(empresaDto.DataFundacao);

                    if (empresa.Validar())
                        await _repositorioDeEmpresa.Alterar(empresa);
                    else
                        _notificationContext.AddNotifications(empresa.Result);
                }
                else
                    _notificationContext.AddNotification("", Resource.EmpresaNaoLocalizada);
            }
        }

        private bool ValidarId(int id, EmpresaDto empresaDto)
        {
            if (id != empresaDto.Id)
            {
                _notificationContext.AddNotification("", Resource.EmpresaNaoIdentificada);
                return false;
            }

            return true;
        }
    }
}
