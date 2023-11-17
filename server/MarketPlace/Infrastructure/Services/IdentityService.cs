using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _accessor;

    public IdentityService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    public Guid GetUserIdentity()
    {
        var userIdentity = _accessor.HttpContext?.User.FindFirst("sub")?.Value;
        
        return Guid.Parse(userIdentity!);
    }
}