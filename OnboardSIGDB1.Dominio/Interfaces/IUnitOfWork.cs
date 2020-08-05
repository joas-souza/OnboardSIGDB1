using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
