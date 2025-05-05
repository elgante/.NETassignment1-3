using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTO;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreateDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.WriterId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.WriterId} was not found.");
        }

        

        ValidatePost(dto);
        Post post = new Post(user, dto.Title);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    private void ValidatePost(PostCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title can not be empty.");
        // other validation stuff
    }

    

        public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
        {
            return postDao.GetAsync(searchParameters);
            
        }
        
        
        
        public async Task UpdateAsync(PostUpdateDto dto)
        {
            Post? existing = await postDao.GetByIdAsync(dto.Id);

            if (existing == null)
            {
                throw new Exception($"Post with ID {dto.Id} not found!");
            }

            User? user = null;
            if (dto.WriterId != null)
            {
                user = await userDao.GetByIdAsync((int)dto.WriterId);
                if (user == null)
                {
                    throw new Exception($"User with id {dto.WriterId} was not found.");
                }
            }

            if (dto.IsCompleted != null && existing.IsCompleted && !(bool)dto.IsCompleted)
            {
                throw new Exception("Cannot un-complete a completed Post");
            }

            User userToUse = user ?? existing.Writer;
            string titleToUse = dto.Title ?? existing.Title;
            bool completedToUse = dto.IsCompleted ?? existing.IsCompleted;
        
            Post updated = new (userToUse, titleToUse)
            {
                IsCompleted = completedToUse,
                Id = existing.Id,
            };

            ValidatePost(updated);

            await postDao.UpdateAsync(updated);
        }

        private void ValidatePost(Post dto)
        {
            if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
            // other validation stuff
        }
        
        
        public async Task DeleteAsync(int id)
        {
            Post? post = await postDao.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception($"Post with ID {id} was not found!");
            }

            if (!post.IsCompleted)
            {
                throw new Exception("Cannot delete un-completed Post!");
            }

            await postDao.DeleteAsync(id);
        }
    
        public async Task<PostBasicDto> GetByIdAsync(int id)
        {
            Post? post = await postDao.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception($"Post with id {id} not found");
            }

            return new PostBasicDto(post.Id, post.Writer.UserName, post.Title, post.IsCompleted);
        }


}
