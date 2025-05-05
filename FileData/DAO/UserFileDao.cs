
using Application.DAOInterfaces;
using Domain.DTO;
using Domain.Models;


namespace FileData.DAO;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);

    }
/*If there currently are no Users in the storage, then we just set the Id of the new User to be 1.
Otherwise: The Max() method looks through all the User objects and returns the max value found from the property Id. The result is incremented, and so we know this int is not currently in use as an ID.

The return statement is a bit iffy, because the method signature says to return a Task, but we are not doing anything asynchronous.
Remember, the Task return type is because later on, when we add a real database, these methods will have to do asynchronous work against the database.

But for now, it is synchronous code, looking like asynchronous. The consequence is just that we have to manually wrap the return value in a Task.*/
    
    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing =
            context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);

    }
/*We implemented the IUserDao interface from the Application component.

We receive an instance of FileContext through constructor dependency injection.*/
/*The FirstOrDefault() method will find the first object matching the criteria specified in the lambda expression.
If nothing is found, null is returned.

In the Equals method I specify that the matching should not consider upper/lower case. I don't want a user called Troels and another troels.*/

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UserNameContains != null)
        {
            users = context.Users.Where(u =>
                u.UserName.Contains(searchParameters.UserNameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
//Given an Id we want to return the associated User, or null if none is found. 
    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existing);
    }
    
    
    /*
    public Task DeleteAsync(int id)
    {
        Post? existing = context.Post.FirstOrDefault(post => post.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }*/

}