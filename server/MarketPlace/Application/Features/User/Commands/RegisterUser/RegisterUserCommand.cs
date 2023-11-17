using System.Xml.Linq;
using Application.Common.Mappings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Result<Guid>>, IMapFrom<Domain.Identities.User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmedPassword { get; set; }
}

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly UserManager<Domain.Identities.User> _userManager;

    public RegisterUserCommandHandler(UserManager<Domain.Identities.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new Domain.Identities.User
        {
            UserName = command.UserName,
            Email = command.Email,
            EmailConfirmed = true
        };

        var identityResult = await _userManager.CreateAsync(user, command.Password);

        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors
                .Select(x => x.Description)
                .ToList();
            return await Result<Guid>.FailureAsync(errors);
        }

        return await Result<Guid>.SuccessAsync(user.Id, "registered successfully");
    }
}