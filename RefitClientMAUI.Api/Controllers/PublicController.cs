using Microsoft.AspNetCore.Mvc;
using RefitClientMAUI.Api.Services;
using RefitClientMAUI.Shared;

namespace RefitClientMAUI.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class PublicController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<PublicController> _logger;
    private readonly ITokenService _tokenService;

    public PublicController(ILogger<PublicController> logger, ITokenService tokenService)
    {
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpGet("weathers")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("login")]
    public AuthResponseDto Login(LoginRequestDto loginRequestDto)
    {
        var user = new LoggedInUser(Guid.NewGuid(), "Abhay Prince", "Admin", "randomemail@gmai.com");
        var token = _tokenService.GenerateJWT(user);

        var response = new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            Token = token,
        };
        return response;
    }
}
