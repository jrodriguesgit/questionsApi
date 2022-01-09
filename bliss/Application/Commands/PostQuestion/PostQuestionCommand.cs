using MediatR;
using Shared.ResponsesDtos;
using Shared.RequestsDtos;
using System.Collections.Generic;

namespace Application.Commands.PostQuestion
{
    public record PostQuestionCommand(RequestQuestionDto question)
       : IRequest<ResponseQuestionDto>;
}
