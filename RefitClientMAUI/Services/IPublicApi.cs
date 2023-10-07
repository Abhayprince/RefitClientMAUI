using Refit;
using RefitClientMAUI.Shared;

namespace RefitClientMAUI.Services;
public interface IPublicApi
{
    [Get("/public/weathers")]
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync();

    [Post("/public/login")]
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}
