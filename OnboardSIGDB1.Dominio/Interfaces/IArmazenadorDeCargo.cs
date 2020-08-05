using OnboardSIGDB1.Dominio.Dtos.Cargo;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IArmazenadorDeCargo
    {
        Task Salvar(CargoDto cargoDto);

    }
}
