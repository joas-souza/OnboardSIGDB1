using OnboardSIGDB1.Dominio.Dtos.Cargo;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IConsultasDeCargo
    {
        IEnumerable<CargoDto> RecuperarTodos();

        IEnumerable<CargoDto> RecuperarPorFiltro(Filtro filtro);
    }
}
