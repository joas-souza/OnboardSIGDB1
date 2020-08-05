using OnboardSIGDB1.Dominio.Dtos.Cargo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IConsultasDeCargo
    {
        Task<IEnumerable<CargoDto>> RecuperarTodos();

        Task<IEnumerable<CargoDto>> RecuperarPorFiltro(Filtro filtro);

        Task<CargoDto> RecuperarPorId(int id);
    }
}
