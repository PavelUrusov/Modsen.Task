using AutoMapper;
using MediatR;
using Store.Application.Common;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.All;

public class ReadAllCategoryHandler : IRequestHandler<ReadAllCategoryQuery, ResponseBase>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    public ReadAllCategoryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBase> Handle(ReadAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.ReadAllAsync(cancellationToken);
        var response = _mapper.Map<ReadAllCategoryResponse>(categories);
        return response;
    }
}