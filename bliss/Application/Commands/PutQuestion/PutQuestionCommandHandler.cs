using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Shared.ResponsesDtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.PutQuestion
{
    public class PutQuestionCommandHandler : IRequestHandler<PutQuestionCommand, ResponseQuestionDto>
    {
        private readonly ApiDbContext dbContext;
        private readonly IMapper _mapper;

        public PutQuestionCommandHandler(
            IDbContextFactory<ApiDbContext> sqlWorkflowContextFactory,
            IMapper mapper)
        {
            dbContext = sqlWorkflowContextFactory.CreateDbContext() ?? throw new ArgumentNullException(nameof(sqlWorkflowContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseQuestionDto> Handle(PutQuestionCommand request, CancellationToken cancellationToken)
        {
            Question question = await dbContext.Questions.Include(x => x.Choices).FirstOrDefaultAsync(x => x.Id == request.question.id, cancellationToken);

            question.UpdateImageUrl(request.question.image_url);
            question.UpdateThumbUrl(request.question.thumb_url);
            question.UpdateDescription(request.question.question);

            question.RemoveChoices();

            foreach (var choic in request.question.choices)
            {
                PossibleAnswer ans = new PossibleAnswer(answer: choic.choice, choic.votes, questionId: question.Id);

                question.AddChoice(ans);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            ResponseQuestionDto toReturn = _mapper.Map<ResponseQuestionDto>(question);

            return toReturn;
        }
    }
}
