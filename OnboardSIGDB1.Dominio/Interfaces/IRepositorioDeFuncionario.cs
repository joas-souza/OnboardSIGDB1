
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRepositorioDeFuncionario
    {
        Task Salvar(Funcionario funcionario);

        Task Alterar(Funcionario funcionario);

        Task Excluir(int id);

        Task<Funcionario> RecuperarPorId(int id);

        Task<Funcionario> RecuperarPorCpf(string cpf);
    }
}
