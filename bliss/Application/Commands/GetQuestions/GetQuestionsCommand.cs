using MediatR;
using Shared.ResponsesDtos;
using System.Collections.Generic;

namespace Application.Commands.GetQuestions
{
    public record GetQuestionsCommand(int? limit, int? offset, string filter)
       : IRequest<List<ResponseQuestionDto>>;
}
