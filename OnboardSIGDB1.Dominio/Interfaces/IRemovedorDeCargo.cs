using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRemovedorDeCargo
    {
        Task Excluir(int id);
    }
}
