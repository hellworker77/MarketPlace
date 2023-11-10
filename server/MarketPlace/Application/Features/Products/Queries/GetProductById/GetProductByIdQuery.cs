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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<GetProductByIdDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Product>().GetByIdAsync(query.Id);
        var product = _mapper.Map<GetProductByIdDto>(entity);
        return await Result<GetProductByIdDto>.SuccessAsync(product);
    }
}
