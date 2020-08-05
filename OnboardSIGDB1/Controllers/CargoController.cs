using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;

namespace OnboardSIGDB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly IArmazenadorDeCargo _armazenadorDeCargo;
        private readonly IAlteradorDeCargo _alteradorDeCargo;
        private readonly IRemovedorDeCargo _removedorDeCargo;
        private readonly IConsultasDeCargo _consultasDeCargo;
        private readonly NotificationContext _notificationContext;

        public CargoController(IArmazenadorDeCargo armazenadorDeCargo, IAlteradorDeCargo alteradorDeCargo, IRemovedorDeCargo removedorDeCargo, IConsultasDeCargo consultasDeCargo, NotificationContext notificationContext)
        {
            _armazenadorDeCargo = armazenadorDeCargo;
            _alteradorDeCargo = alteradorDeCargo;
            _removedorDeCargo = removedorDeCargo;
            _consultasDeCargo = consultasDeCargo;
        }

        /// <summary>
        /// Lista todos os cargos.
        /// </summary>
        /// <returns>Lista de cargos.</returns>
        /// <response code="200">Cargos cadastrados.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cargos = await _consultasDeCargo.RecuperarTodos();

                return Ok(cargos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista cargo por Id.
        /// </summary>
        /// <returns>cargo por Id</returns>
        /// <response code="200">cargo cadastrado.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cargo = await _consultasDeCargo.RecuperarPorId(id);

                return Ok(cargo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista cargos por filtro.
        /// </summary>
        /// <returns>Cargos por filtro</returns>
        /// <response code="200">Cargos cadastrados por filtro.</response>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] Filtro filtro)
        {
            try
            {
                var cargos = await _consultasDeCargo.RecuperarPorFiltro(filtro);

                return Ok(cargos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Salvar cargo.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o cargo que acabou de ser inserido.</response>
        /// <response code="404"></response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoDto dto)
        {
            try
            {
                var cargo = await _consultasDeCargo.RecuperarPorFiltro(new Filtro { Descricao = dto.Descricao});
                if(cargo.Count() > 0)
                    _notificationContext.AddNotification("","Cargo já cadastrado");
                else
                    await _armazenadorDeCargo.Salvar(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alterar cargo.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o cargo que acabou de ser atualizado.</response>
        /// <response code="404"></response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CargoDto dto)
        {
            try
            {
                await _alteradorDeCargo.Alterar(id, dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Excluir cargo.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Se o cargo for excluído.</response>
        /// <response code="404">Se o cargo não existir.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _removedorDeCargo.Excluir(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}