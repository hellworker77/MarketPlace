using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Products.Queries.GetAllProducts;

public record GelAllProductsQuery : IRequest<Result<List<GelAllProductsDto>>>;

internal class GelAllProductsQueryHandler : IRequestHandler<GelAllProductsQuery, Result<List<GelAllProductsDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GelAllProductsQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GelAllProductsDto>>> Handle(GelAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().Entities
            .ProjectTo<GelAllProductsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return await Result<List<GelAllProductsDto>>.SuccessAsync(products);
    }
}