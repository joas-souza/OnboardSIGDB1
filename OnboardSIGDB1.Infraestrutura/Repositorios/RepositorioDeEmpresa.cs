using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Repositorios
{
    public class RepositorioDeEmpresa : IRepositorioDeEmpresa
    {
        private readonly OnboardDbContext _contexto;

        public RepositorioDeEmpresa(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public async Task Salvar(Empresa empresa)
        {
            await _contexto.Empresas.AddAsync(empresa);
        }

        public async Task Alterar(Empresa empresa)
        {
            _contexto.Entry(empresa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task Excluir(int id)
        {
            _contexto.Empresas.Remove(await RecuperarPorId(id));
        }

        public async Task<Empresa> RecuperarPorId(int id)
        {
            return await _contexto.Empresas.FindAsync(id);
        }
    }
}
