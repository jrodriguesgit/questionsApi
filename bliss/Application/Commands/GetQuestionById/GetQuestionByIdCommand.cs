using MediatR;
using Shared.ResponsesDtos;

namespace Application.Commands.GetQuestionById
{
    public record GetQuestionByIdCommand(int id)
      : IRequest<ResponseQuestionDto>;
}
