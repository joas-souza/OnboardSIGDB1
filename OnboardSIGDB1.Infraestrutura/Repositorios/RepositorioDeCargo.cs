using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System.Collections.Generic;
using System.Linq;


namespace OnboardSIGDB1.Infraestrutura.Repositorios
{
    public class RepositorioDeCargo : IRepositorioDeCargo
    {
        private readonly OnboardDbContext _contexto;

        public RepositorioDeCargo(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public void Salvar(Cargo cargo)
        {
            _contexto.Cargos.Add(cargo);
            _contexto.SaveChanges();
        }

        public void Alterar(Cargo cargo)
        {
            _contexto.Entry(cargo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Excluir(int id)
        {
            _contexto.Cargos.Remove(RecuperarPorId(id));
            _contexto.SaveChanges();
        }

        public Cargo RecuperarPorId(int id)
        {
            return _contexto.Cargos.Find(id);
        }
    }
}
