using OnboardSIGDB1.Dominio.Entidades;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRepositorioDeEmpresa
    {
        Task Salvar(Empresa empresa);

        Task Alterar(Empresa empresa);

        Task Excluir(int id);

        Task<Empresa> RecuperarPorId(int id);
    }
}
