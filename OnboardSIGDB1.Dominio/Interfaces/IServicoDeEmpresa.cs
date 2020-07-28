using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IServicoDeEmpresa
    {
        EmpresaDto Salvar(EmpresaDto empresaDto);

        EmpresaDto Alterar(int id, EmpresaDto empresaDto);

        void Excluir(int id);

        EmpresaDto RecuperarPorId(int id);
    }
}
