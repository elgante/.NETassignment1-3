using Domain.DTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreateDto dto);
    
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        int? userId, 
        bool? completedStatus, 
        string? titleContains
    );
    
    Task UpdateAsync(PostUpdateDto dto);
    
    Task<PostBasicDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}