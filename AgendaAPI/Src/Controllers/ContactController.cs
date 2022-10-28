using AgendaAPI.Src.Dtos;
using AgendaAPI.Src.Dtos.ContactDto;
using AgendaAPI.Src.Exceptions;
using AgendaAPI.Src.Models;
using AgendaAPI.Src.Repository;
using AgendaAPI.Src.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger, ContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }



        /// <summary>
        /// Listar todos Contatos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Sucess</response>
        /// <response code="400">ErrorException</response>
        /// <response code="500">InternalServerError</response>   
        [HttpGet("list-contacts")]
        public async Task<IActionResult> ListContacts() { 
            try
            {
                List<Contact> contacts = await _contactService.ListContacts();

                return Ok(contacts);
            } catch(Exception e)
            {
                _logger.LogError("Ocorreu um erro ao listar contatos", e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto() {
                    Error = "Ocorreu um erro ao listar os contatos",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        /// <summary>
        /// Buscar contato por parametro
        /// </summary>
        /// <param name="filter">Digite qualquer dado referente ao contato</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// GET
        /// Busca por qualquer parâmetro
        /// 
        /// </remarks>
        /// <response code="200">Sucess</response>
        /// <response code="400">ErrorException</response>
        /// <response code="500">InternalServerError</response>   
        [HttpGet("search-contacts")]
        public async Task<IActionResult> Search([FromQuery] string filter)
        {
            try
            {
                List<Contact> contacts = await _contactService.Search(filter);

                return Ok(contacts);

            }
            catch (ErrorException e)
            {
                return StatusCode(e.StatusCode, new ErrorResponseDto()
                {
                    Error = e.Message,
                    Status = e.StatusCode
                });

            }
        }

        /// <summary>
        /// Criar novo Contato
        /// </summary>
        /// <param name="dto">dados de preenchimento para criar Contato</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST 
        /// {
        /// "FirstName": "John",
        /// "LastName": "Doe",
        /// "NickName": "Fulano",
        /// "Phone": "40028922",
        /// "Company": "BomDiaCia",
        /// "Email":"abc@domain.com"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Sucess</response>
        /// <response code="400">ErrorException</response>
        /// <response code="500">InternalServerError</response>        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContactDto dto)
        {
            try
            {
                await _contactService.Create(dto);
                return Ok();
            } catch(ErrorException e)
            {
                return StatusCode(e.StatusCode, new ErrorResponseDto()
                {
                    Error = e.Message,
                    Status = e.StatusCode
                });
            } catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao criar contato" + e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Error = "Ocorreu um erro ao criar contato",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        /// <summary>
        /// Atualizar Contato
        /// </summary>
        /// <param name="dto">dados a serem atualizados</param>
        /// <param name="id"> identificador de contato</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT
        /// {
        /// "FirstName": "John",
        /// "LastName": "Doe",
        /// "NickName": "Fulano",
        /// "Phone": "40028922",
        /// "Company": "BomDiaCia",
        /// "Email":"abc@domain.com"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Sucess</response>
        /// <response code="400">ErrorException</response>
        /// /// <response code="500">InternalServerError</response>
        [HttpPut("updateContact/{id}")]
        public async Task<ActionResult> Update([FromBody] ContactDto dto, [FromRoute] int id)
        {
            try
            {
                await _contactService.Update(dto, id);
                return Ok();
            }catch (ErrorException e)
            {
                return StatusCode(e.StatusCode, new ErrorResponseDto()
                {
                    Error = e.Message,
                    Status = e.StatusCode
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao atualizar contato" + e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Error = "Ocorreu um erro ao atualizar contato",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        /// <summary>
        /// Deletar Contato
        /// </summary>
        /// <param name="id">identificador de Contato</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE
        /// 
        /// "Id": "0000"
        /// 
        ///
        /// </remarks>
        /// <response code="200">Sucess</response>
        /// <response code="400">Exception</response>
        /// /// <response code="500">InternalServerError</response>
        [HttpDelete("deleteContact/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _contactService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

    }
}
