using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Features.Products.Queries.GetProductByTitle;

public record GetProductByTitleQuery(string Title) : IRequest<Result<List<GetProductByTitleDto>>>
{
    public string Title { get; set; } = Title;
}

internal class
    GetProductByTitleQueryHandler : IRequestHandler<GetProductByTitleQuery, Result<List<GetProductByTitleDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByTitleQueryHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<Result<List<GetProductByTitleDto>>> Handle(GetProductByTitleQuery query,
        CancellationToken cancellationToken)
    {
        var entities = await _productRepository.GetProductsByTitleAsync(query.Title);
        var products = _mapper.Map<List<GetProductByTitleDto>>(entities);
        return await Result<List<GetProductByTitleDto>>.SuccessAsync(products);
    }
}