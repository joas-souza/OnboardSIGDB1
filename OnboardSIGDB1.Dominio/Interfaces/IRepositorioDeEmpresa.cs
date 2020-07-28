using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IRepositorioDeEmpresa
    {
        void Salvar(Empresa empresa);

        void Alterar(Empresa empresa);

        void Excluir(int id);

        Empresa RecuperarPorId(int id);
    }
}
