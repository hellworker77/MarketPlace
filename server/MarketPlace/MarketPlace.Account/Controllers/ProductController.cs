using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetProductsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace MarketPlace.Account.Controllers;

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
    [Route("paged")]
    public async Task<ActionResult<PaginatedResult<GetProductsWithPaginationDto>>> GetProductsWithPagination(
        [FromQuery] GetProductsWithPaginationQuery query)
    {
        var validator = new GetProductsWithPaginationValidator();

        var result = await validator.ValidateAsync(query);

        if (result.IsValid) return await _mediator.Send(query);

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        return BadRequest(errorMessages);
    }

    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }
}