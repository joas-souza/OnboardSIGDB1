using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Entidades;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Infraestrutura.Contexto;
using System.Collections.Generic;
using System.Linq;

namespace OnboardSIGDB1.Infraestrutura.Consultas
{
    public class ConsultasDeCargo:IConsultasDeCargo
    {
        private readonly OnboardDbContext _contexto;

        public ConsultasDeCargo(OnboardDbContext onboardDbContext)
        {
            _contexto = onboardDbContext;
        }

        public IEnumerable<CargoDto> RecuperarTodos()
        {
            return _contexto.Cargos.ToList().Select(c => new CargoDto { Id = c.Id, Descricao = c.Descricao });
        }

        public IEnumerable<CargoDto> RecuperarPorFiltro(Filtro filtro)
        {
            return _contexto.Cargos.Where(e => (string.IsNullOrEmpty(filtro.Descricao) || e.Descricao == filtro.Descricao)).ToList().Select(c => new CargoDto { Id = c.Id, Descricao = c.Descricao });
        }
    }
}
