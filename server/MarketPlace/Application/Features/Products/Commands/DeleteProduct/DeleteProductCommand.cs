using Application.Common.Mappings;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;

namespace Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand : IRequest<Result<Guid>>, IMapFrom<Product>
{
    public DeleteProductCommand()
    {
    }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IIdentityService _identityService;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper, 
        IProductRepository productRepository,
        IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
        _identityService = identityService;
    }

    public async Task<Result<Guid>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdWithUserIdAsync(command.Id, _identityService.GetUserIdentity());

        if (product is not null)
        {
            await _unitOfWork.Repository<Product>().DeleteAsync(product);
            product.AddDomainEvent(new ProductDeletedEvent(product));

            await _unitOfWork.Save(cancellationToken);

            return await Result<Guid>.SuccessAsync(product.Id, "Product deleted.");
        }

        return await Result<Guid>.FailureAsync("Product not found");
    }
}