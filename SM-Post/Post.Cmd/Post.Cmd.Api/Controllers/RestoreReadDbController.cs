using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.DTOs;
using Post.Cmd.Domain.Commands;
using Post.Cmd.Domain.Commands.Post;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RestoreReadDbController : ControllerBase
    {
        private readonly ILogger<NewPostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public RestoreReadDbController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> RestoreReadDbAsync()
        {
            try
            {
                await _commandDispatcher.SendAsync(new RestoreReadDbCommand());

                return StatusCode(StatusCodes.Status201Created, new BaseResponce
                {
                    Message = "Read db was successfully restored."
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
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to read db restore";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponce
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
