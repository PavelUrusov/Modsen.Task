using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.Range;

public class ReadRangeCategoryHandler : IRequestHandler<ReadRangeCategoryQuery, ResponseBase>, ILoggingBehavior
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    public ReadRangeCategoryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadRangeCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories =
            await _repository.ReadRangeAsync(request.Skip, request.Take, c => c.Id, cancellationToken: cancellationToken);
        var response = _mapper.Map<ReadRangeCategoryResponse>(categories);
        return response;
    }
}