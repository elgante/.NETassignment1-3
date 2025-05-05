using System.Text.Json;
using Domain.Models;
using FileDate;


namespace FileData;
/*You need to define the path to the file, which should hold the data. And we need two collections, one for Users and one for Todos.*/

/*Line 1 is just the file path.
Line 2 is the DataContainer, which after being loaded, will keep all our data. It is obviously not very efficient or scalable, because we are essentially keeping the entire database in memory. If the database contains a lot of data, we will not have enough memory. However, for this toy example, it is just fine. Notice the variable is nullable, marked with the "?", indicating we allow this field to be null. We will regularly reset the data, clear it out and reload it.

Then two properties. They both attempt to lazy load the data. Then the relevant collection is returned.

The LoadData method will check if the data is loaded. If not, i.e. dataContainer is null, then the data is loaded*/

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            dataContainer = new ()
            {
                Posts = new List<Post>(),
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    
    
    /*The method is private, because this class should be responsible for determining when to load data. No outside class should tell this class to load data.
First we check if the data is already loaded, and if so, we return.
Then we check if there is a file, and if not, we just create a new "empty" DataContainer.
If there is a file: We read all the content of the file, it returns a string. Then that string is deserialized into a DataContainer, and assigned to the field variable.*/

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}