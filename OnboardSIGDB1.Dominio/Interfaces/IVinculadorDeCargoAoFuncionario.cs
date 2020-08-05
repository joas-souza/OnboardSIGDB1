using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IVinculadorDeCargoAoFuncionario
    {
        Task VincularCargo(int id, int cargoId);
    }
}
