using Refit;
using RefitClientMAUI.Shared;

namespace RefitClientMAUI.Services;

[Headers("Authorization: Bearer")]
public interface IPrivateApi
{
    [Get("/private/my-profile")]
    Task<LoggedInUser> GetMyProfileAsync();
}
