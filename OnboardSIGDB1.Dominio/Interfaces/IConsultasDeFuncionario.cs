using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IConsultasDeFuncionario
    {
        Task<IEnumerable<FuncionarioDto>> RecuperarTodos();

        Task<IEnumerable<FuncionarioDto>> RecuperarPorFiltro(Filtro filtro);

        Task<FuncionarioDto> RecuperarPorId(int id);
    }
}
