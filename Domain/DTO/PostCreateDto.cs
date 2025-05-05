namespace Domain.DTO;

public class PostCreateDto
{
    public int WriterId { get; }
    public string Title { get; }

    public PostCreateDto(int writerId, string title)
    {
        WriterId = writerId;
        Title = title;
    }
}