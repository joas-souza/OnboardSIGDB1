using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1.Infraestrutura.Contexto
{
    public class OnboardDbContext : DbContext
    {

        public OnboardDbContext(DbContextOptions<OnboardDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

    }
}
