using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Interfaces;
using OnboardSIGDB1.Dominio.Servicos.Notificacoes;

namespace OnboardSIGDB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IArmazenadorDeEmpresa _armazenadorDeEmpresa;
        private readonly IAlteradorDeEmpresa _alteradorDeEmpresa;
        private readonly IRemovedorDeEmpresa _removedorDeEmpresa;
        private readonly IConsultasDeEmpresa _consultasDeEmpresa;

        public EmpresaController(IArmazenadorDeEmpresa armazenadorDeEmpresa,
                                 IAlteradorDeEmpresa alteradorDeEmpresa,
                                 IRemovedorDeEmpresa removedorDeEmpresa,
                                 IConsultasDeEmpresa consultasDeEmpresa)
        {
            _armazenadorDeEmpresa = armazenadorDeEmpresa;
            _alteradorDeEmpresa = alteradorDeEmpresa;
            _removedorDeEmpresa = removedorDeEmpresa;
            _consultasDeEmpresa = consultasDeEmpresa;
        }

        /// <summary>
        /// Lista todas as empresas.
        /// </summary>
        /// <returns>Lista de Empresas.</returns>
        /// <response code="200">Empresas cadastradas.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var empresas = await _consultasDeEmpresa.RecuperarTodos();

                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista empresa por Id.
        /// </summary>
        /// <returns>Empresa por Id</returns>
        /// <response code="200">Empresa cadastrada.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var empresa = await _consultasDeEmpresa.RecuperarPorId(id);

                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista empresas por filtro.
        /// </summary>
        /// <param name="nome"></param>
        ///<param name="cnpj"></param>
        /// <returns>Empresas por filtro</returns>
        /// <response code="200">Empresas cadastradas por filtro.</response>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> Get([FromQuery]Filtro filtro)
        {
            try
            {
                var empresas = await _consultasDeEmpresa.RecuperarPorFiltro(filtro);

                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Salvar empresa.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna a empresa que acabou de ser inserida.</response>
        /// <response code="404"></response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaDto dto)
        {
            try
            {
                await _armazenadorDeEmpresa.Salvar(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alterar empresa.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna a empresa que acabou de ser atualizada.</response>
        /// <response code="404"></response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmpresaDto dto)
        {
            try
            {
                await _alteradorDeEmpresa.Alterar(id, dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Excluir empresa.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Se a empresa for excluída.</response>
        /// <response code="404">Se a empresa não existir.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _removedorDeEmpresa.Excluir(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}