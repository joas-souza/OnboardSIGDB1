using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Dominio.Dtos.Funcionario;
using OnboardSIGDB1.Dominio.Interfaces;

namespace OnboardSIGDB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IArmazenadorDeFuncionario _armazenadorDeFuncionario;

        private readonly IAlteradorDeFuncionario _alteradorDeFuncionario;
        private readonly IVinculadorDeEmpresaAoFuncionario _vinculadorDeEmpresaAoFuncionario;
        private readonly IVinculadorDeCargoAoFuncionario _vinculadorDeCargoAoFuncionario;
        private readonly IRemovedorDeFuncionario _removedorDeFuncionario;
        private readonly IConsultasDeFuncionario _consultasDeFuncionario;

        public FuncionarioController(IArmazenadorDeFuncionario armazenadorDeFuncionario,
                                    IAlteradorDeFuncionario alteradorDeFuncionario,
                                    IVinculadorDeEmpresaAoFuncionario vinculadorDeEmpresaAoFuncionario,
                                    IVinculadorDeCargoAoFuncionario vinculadorDeCargoAoFuncionario,
                                    IRemovedorDeFuncionario removedorDeFuncionario,
                                    IConsultasDeFuncionario consultasDeFuncionario)
        {
            _armazenadorDeFuncionario = armazenadorDeFuncionario;
            _alteradorDeFuncionario = alteradorDeFuncionario;
            _vinculadorDeEmpresaAoFuncionario = vinculadorDeEmpresaAoFuncionario;
            _vinculadorDeCargoAoFuncionario = vinculadorDeCargoAoFuncionario;
            _removedorDeFuncionario = removedorDeFuncionario;
            _consultasDeFuncionario = consultasDeFuncionario;
        }

        /// <summary>
        /// Lista todos os funcionários.
        /// </summary>
        /// <returns>Lista de funcionários.</returns>
        /// <response code="200">Funcionários cadastradas.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var funcionarios = await _consultasDeFuncionario.RecuperarTodos();

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista funcionário por Id.
        /// </summary>
        /// <returns>Funcionário por Id</returns>
        /// <response code="200">Funcionário cadastrado.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var funcionario = await _consultasDeFuncionario.RecuperarPorId(id);

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista funcionários por filtro.
        /// </summary>
        /// <returns>Funcionários por filtro</returns>
        /// <response code="200">Funcionários cadastrados por filtro.</response>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery] Filtro filtro)
        {
            try
            {
                var funcionarios = await _consultasDeFuncionario.RecuperarPorFiltro(filtro);

                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Salvar funcionário.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o funcionário que acabou de ser inserido.</response>
        /// <response code="404"></response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto dto)
        {
            try
            {
                await _armazenadorDeFuncionario.Salvar(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alterar o funcionário.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o funcionário que acabou de ser atualizado.</response>
        /// <response code="404"></response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto dto)
        {
            try
            {
                await _alteradorDeFuncionario.Alterar(id, dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/vincularempresa/{empresaId}")]
        public async Task<IActionResult> VincularEmpresa(int id, int empresaId)
        {
            try
            {
                await _vinculadorDeEmpresaAoFuncionario.VincularEmpresa(id, empresaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/atribuircargo/{cargoId}")]
        public async Task<IActionResult> AtribuirCargo(int id, int cargoId)
        {
            try
            {

                await _vinculadorDeCargoAoFuncionario.VincularCargo(id, cargoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Excluir o funcionário.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Se o funcionário for excluído.</response>
        /// <response code="404">Se o funcionário não existir.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _removedorDeFuncionario.Excluir(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}