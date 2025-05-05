namespace Domain.DTO;

public class SearchUserParametersDto
{
    public string? UserNameContains { get; }

    public SearchUserParametersDto( string? userNameContains)
    {
        UserNameContains = userNameContains;
    }
}