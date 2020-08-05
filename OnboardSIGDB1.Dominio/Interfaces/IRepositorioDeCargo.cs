using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRepositorioDeCargo
    {
        Task Salvar(Cargo cargo);

        Task Alterar(Cargo cargo);

        Task Excluir(int id);

        Task<Cargo> RecuperarPorId(int id);
    }
}
