
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRepositorioDeFuncionario
    {
        void Salvar(Funcionario funcionario);

        void Alterar(Funcionario funcionario);

        void Excluir(int id);

        Funcionario RecuperarPorId(int id);
    }
}
