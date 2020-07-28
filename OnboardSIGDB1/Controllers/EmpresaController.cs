using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardSIGDB1.Dominio.Dtos.Empresa;
using OnboardSIGDB1.Dominio.Interfaces;

namespace OnboardSIGDB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IServicoDeEmpresa _servicoDeEmpresa;
        private readonly IConsultasDeEmpresa _consultasDeEmpresa;

        public EmpresaController(IServicoDeEmpresa servicoDeEmpresa, IConsultasDeEmpresa consultasDeEmpresa)
        { 
            _servicoDeEmpresa = servicoDeEmpresa;
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
                var empresas = _consultasDeEmpresa.RecuperarTodos();

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
                var empresa = _servicoDeEmpresa.RecuperarPorId(id);

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
                var empresas = _consultasDeEmpresa.RecuperarPorFiltro(filtro);

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
                var empresa = _servicoDeEmpresa.Salvar(dto);

                return Ok(empresa?.Id);
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
                var empresa = _servicoDeEmpresa.Alterar(id, dto);

                return Ok(empresa);
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
                _servicoDeEmpresa.Excluir(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}