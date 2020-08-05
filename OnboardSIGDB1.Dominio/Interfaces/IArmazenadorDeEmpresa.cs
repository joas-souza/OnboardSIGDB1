using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Dominio.Interfaces
{
    public interface IArmazenadorDeEmpresa
    {
        Task Salvar(EmpresaDto empresaDto);
    }
}
