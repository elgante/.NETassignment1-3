namespace Domain.DTO;

public class SearchPostParametersDto
{


    public string? UserName { get;}
    public int? UserId { get;}
    
    public bool? CompletedStatus { get;}
    
    public string? TitleContains { get;}

    public SearchPostParametersDto(string? userName, int? userId,  string? titleContains, bool? completedStatus)
    {
        UserName = userName;
        UserId = userId;
        CompletedStatus = completedStatus;
        TitleContains = titleContains;
    }
    
    
    
} 
