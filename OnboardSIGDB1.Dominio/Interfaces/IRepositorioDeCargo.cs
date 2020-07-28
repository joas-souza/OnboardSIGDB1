using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRepositorioDeCargo
    {
        void Salvar(Cargo cargo);

        void Alterar(Cargo cargo);

        void Excluir(int id);

        Cargo RecuperarPorId(int id);
    }
}
