using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Consultas
{
    public class ConsultasDeFuncionario : IConsultasDeFuncionario
    {
        private readonly OnboardDbContext _contexto;

        public ConsultasDeFuncionario(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public async Task<IEnumerable<FuncionarioDto>> RecuperarTodos()
        {
            var funcionarios = await _contexto.Funcionarios
                .Include(i => i.CargosFuncionario)
                //    .ThenInclude(c => c.Cargo)
                .ToListAsync();

            return Mapper.Map<List<FuncionarioDto>>(funcionarios);
        }

        public async Task<IEnumerable<FuncionarioDto>> RecuperarPorFiltro(Filtro filtro)
        {
            var funcionarios = await _contexto.Funcionarios
                .Include(i => i.CargosFuncionario)
                .Where(e => (string.IsNullOrEmpty(filtro.Nome) || e.Nome == filtro.Nome) &&
                            (string.IsNullOrEmpty(filtro.Cpf) || e.Cpf == filtro.Cpf) &&
                            (filtro.DataContratacao == DateTime.MinValue || e.DataContratacao == filtro.DataContratacao))
                .ToListAsync();

            return Mapper.Map<List<FuncionarioDto>>(funcionarios);
        }

        public async Task<FuncionarioDto> RecuperarPorId(int id)
        {
            var funcionario = await _contexto.Funcionarios
                .Include(i => i.CargosFuncionario)
                .FirstOrDefaultAsync(f => f.Id == id);
            return Mapper.Map<FuncionarioDto>(funcionario);
        }

       
    }
}
