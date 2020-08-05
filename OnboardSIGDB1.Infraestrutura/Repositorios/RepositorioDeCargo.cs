using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Repositorios
{
    public class RepositorioDeCargo : IRepositorioDeCargo
    {
        private readonly OnboardDbContext _contexto;

        public RepositorioDeCargo(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public async Task Salvar(Cargo cargo)
        {
            await _contexto.Cargos.AddAsync(cargo);
        }

        public async Task Alterar(Cargo cargo)
        {
            _contexto.Entry(cargo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task Excluir(int id)
        {
             _contexto.Cargos.Remove(await RecuperarPorId(id));

        }

        public async Task<Cargo> RecuperarPorId(int id)
        {
            return await _contexto.Cargos.FindAsync(id);
        }
    }
}
