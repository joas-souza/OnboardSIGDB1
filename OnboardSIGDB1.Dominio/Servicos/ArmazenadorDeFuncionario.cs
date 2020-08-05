using AutoMapper;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;
using OnboardSIGDB1.Dominio.Utils;
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
            funcionarioDto.Cpf = MetodosUteis.RemoverMascara(funcionarioDto.Cpf);

            var funcionarioCadastrado = await _consultasDeFuncionario.RecuperarPorFiltro(new Filtro { Cpf = funcionarioDto.Cpf });

            if (funcionarioCadastrado.Count()>0)
                _notificationContext.AddNotification("", "Funcionário já cadastrado");
            else
            {
                var funcionario = Mapper.Map<Funcionario>(funcionarioDto);

                if (funcionario.Validar())
                    await _repositorioDeFuncionario.Salvar(funcionario);
                else
                    _notificationContext.AddNotifications(funcionario.Result);
            }
        }
    }
}
