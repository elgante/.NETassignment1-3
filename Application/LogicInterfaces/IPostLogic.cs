using Domain.DTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync (PostCreateDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(PostUpdateDto postUpdate);
    Task DeleteAsync(int id);
    Task<PostBasicDto> GetByIdAsync(int id);
   
}