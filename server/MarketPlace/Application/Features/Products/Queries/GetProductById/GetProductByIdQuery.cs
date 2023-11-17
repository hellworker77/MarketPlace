using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;

namespace Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<GetProductByIdDto>>
{
    public Guid Id { get; set; } = Id;
}

internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetProductByIdDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Product>().GetByIdAsync(query.Id);

        if (entity == null)
            return await Result<GetProductByIdDto>.FailureAsync("Product not fount");

        var product = _mapper.Map<GetProductByIdDto>(entity);
        return await Result<GetProductByIdDto>.SuccessAsync(product);
    }
}