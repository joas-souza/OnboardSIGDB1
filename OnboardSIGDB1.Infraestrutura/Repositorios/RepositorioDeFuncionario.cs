using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Repositorios
{
    public class RepositorioDeFuncionario : IRepositorioDeFuncionario
    {
        private readonly OnboardDbContext _contexto;

        public RepositorioDeFuncionario(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public async Task Salvar(Funcionario funcionario)
        {
            await _contexto.Funcionarios.AddAsync(funcionario);
        }

        public async Task Alterar(Funcionario funcionario)
        {
            _contexto.Entry(funcionario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task Excluir(int id)
        {
            _contexto.Funcionarios.Remove(await RecuperarPorId(id));
        }

        public async Task<Funcionario> RecuperarPorId(int id)
        {
            return await _contexto.Funcionarios.FindAsync(id);
        }

        public async Task<Funcionario> RecuperarPorCpf(string cpf)
        {
            return await _contexto.Funcionarios.FirstOrDefaultAsync(f => f.Cpf == cpf);
        }
    }
}
