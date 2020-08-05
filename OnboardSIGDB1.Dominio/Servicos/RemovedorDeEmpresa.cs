using OnboardSIGDB1.Dominio.Interfaces;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class RemovedorDeEmpresa : IRemovedorDeEmpresa
    {
        private readonly IRepositorioDeEmpresa _repositorioDeEmpresa;

        public RemovedorDeEmpresa(IRepositorioDeEmpresa repositorioDeEmpresa)
        {
            _repositorioDeEmpresa = repositorioDeEmpresa;
        }

        public async Task Excluir(int id)
        {
            if (id > 0)
                await _repositorioDeEmpresa.Excluir(id);
        }
    }
}
