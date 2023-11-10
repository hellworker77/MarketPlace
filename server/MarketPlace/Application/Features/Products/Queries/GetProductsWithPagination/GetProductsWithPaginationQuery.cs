using Application.Extension;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Shared;

namespace Application.Features.Products.Queries.GetProductsWithPagination;

public record class GetProductsWithPaginationQuery : IRequest<PaginatedResult<GetProductsWithPaginationDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetProductsWithPaginationQuery()
    {
        
    }

    public GetProductsWithPaginationQuery(int pageNumber,
        int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

internal class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedResult<GetProductsWithPaginationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PaginatedResult<GetProductsWithPaginationDto>> Handle(GetProductsWithPaginationQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Product>().Entities
            .OrderBy(x=>x.UpdatedDate)
            .ProjectTo<GetProductsWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
    }
}