using Application.Commands.GetQuestionById;
using Application.Commands.GetQuestions;
using Application.Commands.PostQuestion;
using Application.Commands.PutQuestion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestsDtos;
using Shared.ResponsesDtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ResponseQuestionDto>> GetQuestions(
                                                int? limit, 
                                                int? offset, 
                                                string filter)
        {
            var command = new GetQuestionsCommand(limit, offset, filter);

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpGet("{question_id}")]
        public async Task<ResponseQuestionDto> GetQuestion([Required] int question_id)
        {
            var command = new GetQuestionByIdCommand(question_id);

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost]
        public async Task<ResponseQuestionDto> PostQuestion([Required] RequestQuestionDto question)
        {
            var command = new PostQuestionCommand(question);

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPut("{question_id}")]
        public async Task<ResponseQuestionDto> PutQuestion([Required] int question_id, [Required] RequestUpdateQuestionDto question)
        {
            var command = new PutQuestionCommand(question);

            var result = await _mediator.Send(command);

            return result;
        }
    }
}
