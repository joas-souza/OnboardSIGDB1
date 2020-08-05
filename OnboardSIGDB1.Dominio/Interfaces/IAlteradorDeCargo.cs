using OnboardSIGDB1.Dominio.Dtos.Cargo;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IAlteradorDeCargo
    {
        Task Alterar(int id, CargoDto cargoDto);      
    }
}
