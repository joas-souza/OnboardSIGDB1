using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Dominio.Dtos.Cargo;
using OnboardSIGDB1.Dominio.Interfaces;

namespace OnboardSIGDB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly IServicoDeCargo _servicoDeCargo;
        private readonly IConsultasDeCargo _consultasDeCargo;

        public CargoController(IServicoDeCargo servicoDeCargo, IConsultasDeCargo consultasDeCargo)
        {
            _servicoDeCargo = servicoDeCargo;
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
                var cargos = _consultasDeCargo.RecuperarTodos();

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
                var cargo = _servicoDeCargo.RecuperarPorId(id);

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
                //Revisar este item
                var cargos = _consultasDeCargo.RecuperarPorFiltro(filtro);

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
                var cargo = _servicoDeCargo.Salvar(dto);

                return Ok(cargo?.Id);
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
                var cargo = _servicoDeCargo.Alterar(id, dto);

                return Ok(cargo);
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
                _servicoDeCargo.Excluir(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}