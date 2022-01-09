using AutoMapper;
using Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using Persistence;
using Shared.ResponsesDtos;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, ResponseSendEmailDto>
    {
        private readonly IConfiguration Configuration;

        public SendEmailCommandHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<ResponseSendEmailDto> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            bool validEmail = IsValidEmail(request.destination_email);

            if (!validEmail || string.IsNullOrEmpty(request.content_url))
            {
                return new ResponseSendEmailDto() { status = "Bad Request. Either destination_email not valid or empty content_url" };
            }

            #region smtpserver

            using (var smtpClient = new SmtpClient(Configuration["Email:SmtpServer"], Convert.ToInt32(Configuration["Email:Port"])))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential()
                {
                    UserName = Configuration["Email:Username"],
                    Password = Configuration["Email:Password"],
                };
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                smtpClient.SendAsync(Configuration["Email:From"], request.destination_email, "title", request.content_url, null);
            }
            #endregion

            return new ResponseSendEmailDto() { status = "Ok" };

        }
        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
