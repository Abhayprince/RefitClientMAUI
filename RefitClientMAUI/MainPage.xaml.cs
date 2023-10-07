using Refit;
using RefitClientMAUI.Services;
using System.Text.Json;

namespace RefitClientMAUI;

public partial class MainPage : ContentPage
{
    private readonly IPublicApi _publicApi;
    private readonly IPrivateApi _privateApi;
    private readonly TokenService _tokenService;

    public MainPage(IPublicApi publicApi, IPrivateApi privateApi, TokenService tokenService)
    {
        InitializeComponent();
        _publicApi = publicApi;
        _privateApi = privateApi;
        _tokenService = tokenService;
    }

    private async void weatherBtn_Clicked(object sender, EventArgs e)
    {
        var weathers =  await _publicApi.GetWeatherForecastsAsync();
        weatherLbl.Text = JsonSerializer.Serialize(weathers);
    }

    private async void loginBtn_Clicked(object sender, EventArgs e)
    {
        var response = await _publicApi.LoginAsync(new Shared.LoginRequestDto(string.Empty, string.Empty));
        tokenLbl.Text = response.Token;

        _tokenService.SetToken(response.Token);
    }

    private async void profileBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            var response = await _privateApi.GetMyProfileAsync();
            profileLbl.Text = JsonSerializer.Serialize(response);
        }
        catch (ApiException ex)
        {
            profileLbl.Text = ex.Message;
            //throw;
        }
    }
}

