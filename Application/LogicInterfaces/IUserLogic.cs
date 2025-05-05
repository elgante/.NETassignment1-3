using Domain.DTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreateDto userToCreate);
   Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    
}