using Application.DAOInterfaces;
using Domain.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        return post;
    }
   

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Writer).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParams.UserName))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.Writer.UserName.ToLower().Equals(searchParams.UserName.ToLower()));
        }
    
        if (searchParams.UserId != null)
        {
            query = query.Where(t => t.Writer.Id == searchParams.UserId);
        }
    
        if (searchParams.CompletedStatus != null)
        {
            query = query.Where(t => t.IsCompleted == searchParams.CompletedStatus);
        }
    
        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParams.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post post)
    {
        context.ChangeTracker.Clear();
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? post = await context.Posts.FindAsync(postId);
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }
}