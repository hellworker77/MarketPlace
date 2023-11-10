using Application.Common.Mappings;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;

namespace Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand : IRequest<Result<Guid>>, IMapFrom<Product>
{
    public Guid Id { get; set; }

    public DeleteProductCommand()
    {
    }

    public DeleteProductCommand(Guid id)
    {
        id = id;
    }
}

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);

        if (product is not null)
        {
            await _unitOfWork.Repository<Product>().DeleteAsync(product);
            product.AddDomainEvent(new ProductDeletedEvent(product));

            await _unitOfWork.Save(cancellationToken);

            return await Result<Guid>.SuccessAsync(product.Id, "Product deleted.");
        }
        else
        {
            return await Result<Guid>.FailureAsync("Product not fount");
        }
    }
}