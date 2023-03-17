using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.DTOs;
using Post.Cmd.Domain.Commands.Comment;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EditCommentController : ControllerBase
    {
        private readonly ILogger<EditCommentController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public EditCommentController(ILogger<EditCommentController> logger, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCommentAsync(Guid id, EditCommentCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);
                return Ok(new BaseResponce
                {
                    Message = "Comment was successfully edited."
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
                const string SAFE_ERROR_MESSAGE = "Error while processing request to edit a comment to a post";
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
