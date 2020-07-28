using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IConsultasDeFuncionario
    {
        IEnumerable<FuncionarioDto> RecuperarTodos();

        IEnumerable<FuncionarioDto> RecuperarPorFiltro(Filtro filtro);
    }
}
