using OnboardSIGDB1.Dominio.Interfaces;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Servicos
{
    public class RemovedorDeFuncionario : IRemovedorDeFuncionario
    {
        private readonly IRepositorioDeFuncionario _repositorioDeFuncionario;

        public RemovedorDeFuncionario(IRepositorioDeFuncionario repositorioDeFuncionario)
        {
            _repositorioDeFuncionario = repositorioDeFuncionario;
        }

        public async Task Excluir(int id)
        {
            if (id > 0)
                await _repositorioDeFuncionario.Excluir(id);
        }
    }
}
