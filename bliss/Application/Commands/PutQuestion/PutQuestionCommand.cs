using MediatR;
using Shared.ResponsesDtos;
using Shared.RequestsDtos;

namespace Application.Commands.PutQuestion
{
    public record PutQuestionCommand(RequestUpdateQuestionDto question)
       : IRequest<ResponseQuestionDto>;
}
