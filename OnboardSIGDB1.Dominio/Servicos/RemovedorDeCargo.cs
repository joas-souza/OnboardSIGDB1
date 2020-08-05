using OnboardSIGDB1.Dominio.Interfaces;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class RemovedorDeCargo : IRemovedorDeCargo
    {
        private readonly IRepositorioDeCargo _repositorioDeCargo;

        public RemovedorDeCargo(IRepositorioDeCargo repositorioDeCargo)
        {
            _repositorioDeCargo = repositorioDeCargo;
        }

        public async Task Excluir(int id)
        {
            if (id > 0)
               await _repositorioDeCargo.Excluir(id);
        }
    }
}
