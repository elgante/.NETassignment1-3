using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    public User Writer { get; private set; }
    
    [MaxLength(50)]
    public string Title { get; private set; }
    public bool IsCompleted { get; set; }

    public Post(User writer, string title)
    {
      Writer = writer;
        Title = title;
    }
    private Post(){}
    
    
}
