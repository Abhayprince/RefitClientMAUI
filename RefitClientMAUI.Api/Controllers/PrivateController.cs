using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefitClientMAUI.Shared;
using System.Security.Claims;

namespace RefitClientMAUI.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PrivateController : ControllerBase
{
    [HttpGet("my-profile")]
    public LoggedInUser GetMyProfile()
    {
        var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var name = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        var role = User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        var email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

        return new LoggedInUser(userId, name, role, email);
    }
}
