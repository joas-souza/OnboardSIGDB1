using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Infraestrutura.Consultas
{
    public class ConsultasDeEmpresa : IConsultasDeEmpresa
    {
        private readonly OnboardDbContext _contexto;

        public ConsultasDeEmpresa(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        IEnumerable<EmpresaDto> IConsultasDeEmpresa.RecuperarTodos()
        {
            return _contexto.Empresas.ToList().Select(e => new EmpresaDto { Id = e.Id, Nome = e.Nome, Cnpj = e.Cnpj, DataFundacao = e.DataFundacao });
        }

        public IEnumerable<EmpresaDto> RecuperarPorFiltro(Filtro filtro)
        {
            return _contexto.Empresas.Where(e => (string.IsNullOrEmpty(filtro.Nome) || e.Nome == filtro.Nome) &&
                                                 (string.IsNullOrEmpty(filtro.Cnpj) || e.Cnpj == filtro.Cnpj) &&
                                                 (filtro.DataFundacao == DateTime.MinValue || e.DataFundacao == filtro.DataFundacao)).ToList()
                                                 .Select(e => new EmpresaDto { Id = e.Id, Nome = e.Nome, Cnpj = e.Cnpj, DataFundacao = e.DataFundacao });
        }
    }
}
