namespace RefitClientMAUI.Shared;

public readonly record struct LoggedInUser(Guid Id, string Name, string Role, string Email);
