using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.Single;

internal class ReadSingleCategoryHandler : IRequestHandler<ReadSingleCategoryQuery, ResponseBase>, ILoggingBehavior
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    public ReadSingleCategoryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadSingleCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.ReadAsync(request.Id, cancellationToken);
        var response = _mapper.Map<ReadSingleCategoryResponse>(category);
        return response;
    }
}