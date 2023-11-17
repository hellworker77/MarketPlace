using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.GetProductByTitle;
using Application.Features.Products.Queries.GetProductsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace MarketPlace.Commerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<GelAllProductsDto>>>> Get()
    {
        return await _mediator.Send(new GelAllProductsQuery());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Result<GetProductByIdDto>>> GetProductById(Guid id)
    {
        return await _mediator.Send(new GetProductByIdQuery(id));
    }

    [HttpGet]
    [Route("title/{title}")]
    public async Task<ActionResult<Result<List<GetProductByTitleDto>>>> GetProductsByTitle(string title)
    {
        return await _mediator.Send(new GetProductByTitleQuery(title));
    }

    [HttpGet]
    [Route("paged")]
    public async Task<ActionResult<PaginatedResult<GetProductsWithPaginationDto>>> GetProductsWithPagination(
        [FromQuery] GetProductsWithPaginationQuery query)
    {
        var validator = new GetProductsWithPaginationValidator();

        var result = await validator.ValidateAsync(query);

        if (result.IsValid) return await _mediator.Send(query);

        var errorMessages = result.Errors
            .Select(x => x.ErrorMessage)
            .ToList();
        return BadRequest(errorMessages);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Result<Guid>>> Update(UpdateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Result<Guid>>> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteProductCommand(id));
    }
}