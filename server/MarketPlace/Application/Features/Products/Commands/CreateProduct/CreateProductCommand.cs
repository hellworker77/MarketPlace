using Application.Common.Mappings;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;

#pragma warning disable CS8618
namespace Application.Features.Products.Commands.CreateProduct;

public record class CreateProductCommand : IRequest<Result<Guid>>, IMapFrom<Product>
{
    public string Title { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public byte Rate { get; set; }
    public int RemainingCount { get; set; }
}

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var product = new Product
        {
            Title = command.Title,
            Price = command.Price,
            Description = command.Description,
            Category = command.Category,
            Rate = command.Rate,
            RemainingCount = command.RemainingCount,
            UserId = userId
        };

        await _unitOfWork.Repository<Product>().AddAsync(product);
        product.AddDomainEvent(new ProductCreatedEvent(product));

        await _unitOfWork.Save(cancellationToken);

        return await Result<Guid>.SuccessAsync(product.Id, "Product created.");
    }
}