using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;
using Shared.ResponsesDtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.PostQuestion
{
    public class PostQuestionCommandHandler : IRequestHandler<PostQuestionCommand, ResponseQuestionDto>
    {
        private readonly ApiDbContext dbContext;
        private readonly IMapper _mapper;

        public PostQuestionCommandHandler(
            IDbContextFactory<ApiDbContext> sqlWorkflowContextFactory,
            IMapper mapper)
        {
            dbContext = sqlWorkflowContextFactory.CreateDbContext() ?? throw new ArgumentNullException(nameof(sqlWorkflowContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseQuestionDto> Handle(PostQuestionCommand request, CancellationToken cancellationToken)
        {
            Question question = new Question(
                                            imageUrl: request.question.image_url,
                                            thumbUrl: request.question.thumb_url,
                                            description: request.question.question
                                            );
            dbContext.Add(question);

            foreach (var choic in request.question.choices)
            {
                PossibleAnswer ans = new PossibleAnswer(answer: choic, questionId: question.Id);

                question.AddChoice(ans);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            ResponseQuestionDto toReturn = _mapper.Map<ResponseQuestionDto>(question);

            return toReturn;
        }
    }
}
