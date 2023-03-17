using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.DTOs;
using Post.Cmd.Domain.Commands.Post;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeletePostController : ControllerBase
    {
        private readonly ILogger<DeletePostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public DeletePostController(ILogger<DeletePostController> logger, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostAsync(Guid id, DeletePostCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);
                return Ok(new BaseResponce
                {
                    Message = "Post was successfully deleted."
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
            catch (AggregateNotFoundException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate");
                return BadRequest(new BaseResponce
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to delete a post";
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
