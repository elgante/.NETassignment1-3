

namespace HttpClients.ClientInterfaces;
using Domain.DTO;
using Domain.Models;


public interface IUserService
{
    Task<User> Create (UserCreateDto dto);
   
    Task<IEnumerable<User>> GetUsers (string? usernameContains = null);

}