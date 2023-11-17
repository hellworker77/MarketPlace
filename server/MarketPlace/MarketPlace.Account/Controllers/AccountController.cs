using Application.Features.User.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace MarketPlace.Account.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create(RegisterUserCommand command)
    {
        var validator = new RegisterUserCommandValidator();

        var result = await validator.ValidateAsync(command);

        if (result.IsValid) return await _mediator.Send(command);

        var errorMessages = result.Errors
            .Select(x => x.ErrorMessage)
            .ToList();
        return BadRequest(errorMessages);
    }
}