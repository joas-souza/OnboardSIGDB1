using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardSIGDB1.Infraestrutura.Consultas
{
    public class ConsultasDeCargo:IConsultasDeCargo
    {
        private readonly OnboardDbContext _contexto;

        public ConsultasDeCargo(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public async Task<IEnumerable<CargoDto>> RecuperarTodos()
        {
            var cargos = await _contexto.Cargos.ToListAsync();

            return Mapper.Map<List<CargoDto>>(cargos);
        }

        public async Task<IEnumerable<CargoDto>> RecuperarPorFiltro(Filtro filtro)
        {
            var cargos =  await _contexto.Cargos
                .Where(e => (string.IsNullOrEmpty(filtro.Descricao) || e.Descricao == filtro.Descricao))
                .ToListAsync();

            return Mapper.Map<List<CargoDto>>(cargos);
        }

        public async Task<CargoDto> RecuperarPorId(int id)
        {
            var cargo = await _contexto.Cargos.FindAsync(id);

            return Mapper.Map<CargoDto>(cargo);
        }
    }
}
