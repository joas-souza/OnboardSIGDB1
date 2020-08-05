using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Consultas
{
    public class ConsultasDeEmpresa : IConsultasDeEmpresa
    {
        private readonly OnboardDbContext _contexto;

        public ConsultasDeEmpresa(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public async Task<IEnumerable<EmpresaDto>> RecuperarTodos()
        {
            var empresas = await _contexto.Empresas.ToListAsync();

            return Mapper.Map<List<EmpresaDto>>(empresas);
        }

        public async Task<IEnumerable<EmpresaDto>> RecuperarPorFiltro(Filtro filtro)
        {
            var empresas = await _contexto.Empresas
                .Where(e => (string.IsNullOrEmpty(filtro.Nome) || e.Nome == filtro.Nome) &&
                        (string.IsNullOrEmpty(filtro.Cnpj) || e.Cnpj == filtro.Cnpj) &&
                        (filtro.DataFundacao == DateTime.MinValue || e.DataFundacao == filtro.DataFundacao))
               .ToListAsync();

            return Mapper.Map<List<EmpresaDto>>(empresas);
        }

        public async Task<EmpresaDto> RecuperarPorId(int id)
        {
            var empresa = await _contexto.Empresas.FindAsync(id);
            return Mapper.Map<EmpresaDto>(empresa);
        }
    }
}
