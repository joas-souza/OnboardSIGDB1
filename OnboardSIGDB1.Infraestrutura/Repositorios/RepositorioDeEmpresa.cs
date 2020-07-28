using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OnboardSIGDB1.Infraestrutura.Repositorios
{
    public class RepositorioDeEmpresa : IRepositorioDeEmpresa
    {
        private readonly OnboardDbContext _contexto;

        public RepositorioDeEmpresa(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public void Salvar(Empresa empresa)
        {
            _contexto.Empresas.Add(empresa);
            _contexto.SaveChanges();
        }

        public void Alterar(Empresa empresa)
        {
            _contexto.Entry(empresa).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Excluir(int id)
        {
            _contexto.Empresas.Remove(RecuperarPorId(id));
            _contexto.SaveChanges();
        }

        public Empresa RecuperarPorId(int id)
        {
            return _contexto.Empresas.Find(id);
        }
    }
}
