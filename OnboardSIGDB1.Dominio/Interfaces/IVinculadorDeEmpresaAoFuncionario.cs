using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IVinculadorDeEmpresaAoFuncionario
    {
        Task VincularEmpresa(int id, int empresaId);
    }
}
