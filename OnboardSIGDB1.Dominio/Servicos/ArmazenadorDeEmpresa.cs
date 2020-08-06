using AutoMapper;
using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils;
using OnboardSIGDB1.Utils.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class ArmazenadorDeEmpresa : IArmazenadorDeEmpresa
    {
        private readonly IRepositorioDeEmpresa _repositorioDeEmpresa;
        private readonly IConsultasDeEmpresa _consultasDeEmpresa;
        private readonly NotificationContext _notificationContext;

        public ArmazenadorDeEmpresa(IRepositorioDeEmpresa repositorioDeEmpresa,
                    IConsultasDeEmpresa consultasDeEmpresa,
                    NotificationContext notificationContext)
        {
            _repositorioDeEmpresa = repositorioDeEmpresa;
            _consultasDeEmpresa = consultasDeEmpresa;
            _notificationContext = notificationContext;
        }

        public async Task Salvar(EmpresaDto empresaDto)
        {
            empresaDto.Cnpj = Util.RemoverMascara(empresaDto.Cnpj);

            var empresaCadastrada = await _consultasDeEmpresa.RecuperarPorFiltro(new Filtro { Cnpj = empresaDto.Cnpj });

            if (empresaCadastrada.Count() > 0)
                _notificationContext.AddNotification("", Resource.EmpresaJaCadastrada);
            else
            {
                var empresa = Mapper.Map<Empresa>(empresaDto);

                if (empresa.Validar())
                    await _repositorioDeEmpresa.Salvar(empresa);
                else
                    _notificationContext.AddNotifications(empresa.Result);
            }
        }
    }
}
