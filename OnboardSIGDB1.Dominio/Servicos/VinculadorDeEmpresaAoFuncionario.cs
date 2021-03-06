﻿using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils.Resources;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class VinculadorDeEmpresaAoFuncionario : IVinculadorDeEmpresaAoFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;
        private readonly IRepositorioDeEmpresa _repositorioDeEmpresa;
        private readonly NotificationContext _notificationContext;

        public VinculadorDeEmpresaAoFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario, IRepositorioDeEmpresa repositorioDeEmpresa, NotificationContext notificationContext)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
            _repositorioDeEmpresa = repositorioDeEmpresa;
            _notificationContext = notificationContext;
        }

        public async Task VincularEmpresa(int id, int empresaId)
        {
            var funcionario = await _repositorioDeFuncionario.RecuperarPorId(id);

            if (funcionario != null)
            {
                var empresa = await _repositorioDeEmpresa.RecuperarPorId(empresaId);

                if (empresa != null)
                {
                    funcionario.AlterarEmpresa(empresa);

                }
                else
                    _notificationContext.AddNotification("", Resource.EmpresaNaoLocalizada);
            }
            else
                _notificationContext.AddNotification("", Resource.FuncionarioNaoLocalizado);
        }
    }
}
