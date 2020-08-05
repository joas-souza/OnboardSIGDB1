using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IArmazenadorDeFuncionario
    {
        Task Salvar(FuncionarioDto funcionarioDto);
    }
}
