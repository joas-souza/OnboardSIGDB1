using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IAlteradorDeFuncionario
    {
       Task Alterar(int id, FuncionarioDto FuncionarioDto);
    }
}
