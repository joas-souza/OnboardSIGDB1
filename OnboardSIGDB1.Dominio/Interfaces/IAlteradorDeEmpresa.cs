using OnboardSIGDB1.Dominio.Dtos.Empresa;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IAlteradorDeEmpresa
    {
        Task Alterar(int id, EmpresaDto empresaDto);
    }
}
