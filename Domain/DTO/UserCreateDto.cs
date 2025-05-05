namespace Domain.DTO;

public class UserCreateDto
{
    public string UserName { get; }

    public UserCreateDto(string userName)
    {
        UserName = userName;
    }
}