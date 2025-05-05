using Domain.Models;

namespace FileDate;

public class DataContainer
{
   
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }

}