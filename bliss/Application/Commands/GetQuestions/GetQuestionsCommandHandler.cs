using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.ResponsesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetQuestions
{
    public class GetQuestionsCommandHandler : IRequestHandler<GetQuestionsCommand, List<ResponseQuestionDto>>
    {
        private readonly ApiDbContext dbContext;
        private readonly IMapper _mapper;

        public GetQuestionsCommandHandler(
            IDbContextFactory<ApiDbContext> sqlWorkflowContextFactory,
            IMapper mapper)
        {
            dbContext = sqlWorkflowContextFactory.CreateDbContext() ?? throw new ArgumentNullException(nameof(sqlWorkflowContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ResponseQuestionDto>> Handle(GetQuestionsCommand request, CancellationToken cancellationToken)
        {
            var query = dbContext.Questions.Include(x => x.Choices).AsQueryable();

            //limit
            if (request.limit != null)
            {
                query = query.Take(request.limit.Value);

                //offset
                if (request.offset != null)
                {
                    query = query.Skip(request.offset.Value * request.limit.Value);
                }
            }

            if (!string.IsNullOrEmpty(request.filter))
            {
                query = query.Where(x => x.Description.Contains(request.filter) || x.Choices.Where(y => y.Answer.Contains(request.filter)).Count() > 0);
            }

            var result = await query.ToListAsync(cancellationToken);

            List<ResponseQuestionDto> toReturn = _mapper.Map<List<ResponseQuestionDto>>(result);

            return toReturn;
        }
    }
}
