using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Infraestrutura.Consultas
{
    public class ConsultasDeFuncionario : IConsultasDeFuncionario
    {
        private readonly OnboardDbContext _contexto;

        public ConsultasDeFuncionario(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        IEnumerable<FuncionarioDto> IConsultasDeFuncionario.RecuperarTodos()
        {
            return _contexto.Funcionarios.ToList()
                        .Select(f => new FuncionarioDto { Id = f.Id, Nome = f.Nome, Cpf = f.Cpf, DataContratacao = f.DataContratacao, CargoId = f.CargoId, EmpresaId = f.EmpresaId });
        }

        public IEnumerable<FuncionarioDto> RecuperarPorFiltro(Filtro filtro)
        {
            return _contexto.Funcionarios.Where(e => (string.IsNullOrEmpty(filtro.Nome) || e.Nome == filtro.Nome) &&
                                                     (string.IsNullOrEmpty(filtro.Cpf) || e.Cpf == filtro.Cpf) &&
                                                     (filtro.DataContratacao == DateTime.MinValue || e.DataContratacao == filtro.DataContratacao)).ToList()
                                                     .Select(f => new FuncionarioDto { Id = f.Id, Nome = f.Nome, Cpf = f.Cpf, DataContratacao = f.DataContratacao, CargoId = f.CargoId, EmpresaId = f.EmpresaId });
        }
    }
}
