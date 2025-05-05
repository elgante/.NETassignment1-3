using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    public string UserName { get; set; }
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; }
    
    public string Password { get; set; }
    public string Email { get; set; }
    public string Domain { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int Age { get; set; }
    public int SecurityLevel { get; set; }
    
    
    /*
    where does this go?
    JsonSerializer.Serialize(myObj, new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    });*/

}