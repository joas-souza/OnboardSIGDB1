using AutoMapper;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Utils;
using OnboardSIGDB1.Utils.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class ArmazenadorDeFuncionario : IArmazenadorDeFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;
        private readonly IConsultasDeFuncionario _consultasDeFuncionario;
        private readonly NotificationContext _notificationContext;

        public ArmazenadorDeFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario,
            IConsultasDeFuncionario consultasDeFuncionario,
            NotificationContext notificationContext)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
            _consultasDeFuncionario = consultasDeFuncionario;
            _notificationContext = notificationContext;
        }

        public async Task Salvar(FuncionarioDto funcionarioDto)
        {
            funcionarioDto.Cpf = Util.RemoverMascara(funcionarioDto.Cpf);

            var funcionarioCadastrado = await _repositorioDeFuncionario.RecuperarPorCpf(funcionarioDto.Cpf );

            if (funcionarioCadastrado!=null)
                _notificationContext.AddNotification("", Resource.FuncionarioJaCadastrado);
            else
            {
                var funcionario =new Funcionario(funcionarioDto.Nome, funcionarioDto.Cpf, funcionarioDto.DataContratacao);

                if (funcionario.Validar())
                    await _repositorioDeFuncionario.Salvar(funcionario);
                else
                    _notificationContext.AddNotifications(funcionario.Result);
            }
        }
    }
}
