
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OnboardSIGDB1.Infraestrutura.Repositorios
{
    public class RepositorioDeFuncionario : IRepositorioDeFuncionario
    {
        private readonly OnboardDbContext _contexto;

        public RepositorioDeFuncionario(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public void Salvar(Funcionario funcionario)
        {
            _contexto.Funcionarios.Add(funcionario);
            _contexto.SaveChanges();
        }

        public void Alterar(Funcionario funcionario)
        {
            _contexto.Entry(funcionario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Excluir(int id)
        {
            _contexto.Funcionarios.Remove(RecuperarPorId(id));
            _contexto.SaveChanges();
        }

        public Funcionario RecuperarPorId(int id)
        {
            return _contexto.Funcionarios.Find(id);
        }
    }
}
