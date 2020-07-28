using OnboardSIGDB1.Dominio.Dtos.Empresa;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IConsultasDeEmpresa
    {
        IEnumerable<EmpresaDto> RecuperarTodos();

        IEnumerable<EmpresaDto> RecuperarPorFiltro(Filtro filtro);
    }
}
