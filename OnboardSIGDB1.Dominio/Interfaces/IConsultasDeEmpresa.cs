using OnboardSIGDB1.Dominio.Dtos.Empresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IConsultasDeEmpresa
    {
        Task<IEnumerable<EmpresaDto>> RecuperarTodos();

        Task<IEnumerable<EmpresaDto>> RecuperarPorFiltro(Filtro filtro);

        Task<EmpresaDto> RecuperarPorId(int id);

    }
}
