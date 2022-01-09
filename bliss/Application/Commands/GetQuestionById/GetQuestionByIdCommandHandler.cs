using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.ResponsesDtos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetQuestionById
{
    public class GetQuestionByIdCommandHandler : IRequestHandler<GetQuestionByIdCommand, ResponseQuestionDto>
    {
        private readonly ApiDbContext dbContext;
        private readonly IMapper _mapper;

        public GetQuestionByIdCommandHandler(
            IDbContextFactory<ApiDbContext> sqlWorkflowContextFactory,
            IMapper mapper)
        {
            dbContext = sqlWorkflowContextFactory.CreateDbContext() ?? throw new ArgumentNullException(nameof(sqlWorkflowContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseQuestionDto> Handle(GetQuestionByIdCommand request, CancellationToken cancellationToken)
        {
            var query = dbContext.Questions.Include(x => x.Choices).AsQueryable();

            //filter by id
            query.Where(x => x.Id == request.id);

            var result = await query.FirstAsync(cancellationToken);

            ResponseQuestionDto toReturn = _mapper.Map<ResponseQuestionDto>(result);

            return toReturn;
        }
    }
}
