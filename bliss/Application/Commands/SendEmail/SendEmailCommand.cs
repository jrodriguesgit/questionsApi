using MediatR;
using Shared.ResponsesDtos;

namespace Application.Commands.SendEmail
{
    public record SendEmailCommand(string destination_email, string content_url)
       : IRequest<ResponseSendEmailDto>;
}
