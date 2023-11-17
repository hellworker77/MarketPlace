using Domain.Common;

namespace Application.Features.User.Commands.RegisterUser;

public class UserRegisteredEvent : BaseEvent
{
    public UserRegisteredEvent(Domain.Identities.User user)
    {
        User = user;
    }
    public Domain.Identities.User User { get; set; }
}