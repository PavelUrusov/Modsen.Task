using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.CQRS.Logging.Interfaces;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadRange;

public class ReadCategoriesHandler : IRequestHandler<ReadCategoriesQuery, ResponseBase>, ILoggingBehavior
{

    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    public ReadCategoriesHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories;

        if (request.Skip.HasValue && request.Take.HasValue)
            categories = await _repository.ReadRangeAsync(
                request.Skip.Value, request.Take.Value, c => c.Id, cancellationToken: cancellationToken);
        else
            categories = await _repository.ReadAllAsync(cancellationToken);

        var response = _mapper.Map<ReadCategoriesResponse>(categories);

        return response;
    }

}