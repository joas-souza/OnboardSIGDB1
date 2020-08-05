
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRemovedorDeEmpresa
    {
        Task Excluir(int id);
    }
}
