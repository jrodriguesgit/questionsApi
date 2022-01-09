using Application.Commands.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ShareController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShareController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([Required] string destination_email, [Required] string content_url)
        {
            var command = new SendEmailCommand(destination_email, content_url);

            var result = await _mediator.Send(command);

            if(result.status == "Ok")
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
