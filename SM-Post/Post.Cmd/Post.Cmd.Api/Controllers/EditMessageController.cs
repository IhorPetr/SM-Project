using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.DTOs;
using Post.Cmd.Domain.Commands.Message;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EditMessageController : ControllerBase
    {
        private readonly ILogger<EditMessageController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public EditMessageController(ILogger<EditMessageController> logger, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMessageAsync(Guid id, EditMessageCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);
                return Ok(new BaseResponce
                {
                    Message = "Message was successfully edited."
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client make a bad request");
                return BadRequest(new BaseResponce
                {
                    Message = ex.Message
                });
            }
            catch(AggregateNotFoundException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate");
                return BadRequest(new BaseResponce
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new post";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new NewPostResponce
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
