namespace Domain.DTO;

public class PostUpdateDto
{
    public int Id { get; }
    public int? WriterId { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}