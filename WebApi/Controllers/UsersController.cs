using Application.LogicInterfaces;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;
/*This class will be responsible for everything User object related. */
/*Then we have the attribute [ApiController]. This attribute marks this class as a Web API controller, so that the Web API framework will know about our class.

The next attribute [Route("[controller]")] specifies the sub-URI to access this controller class. With that "route template", the URI will be localhost:port/users. If we rename our UserController to something else, the path will be changed too.
We can define our own path with fx [Route("api/users")], and then the URI would be localhost:port/api/users. It is up to you whether you just stick to the default name, or pick something else.*/
[ApiController]
[Route("[controller]")]

/*The class extends ControllerBase to get access to various utility methods.

Then a field variable, injected through the constructor, so we can get access to the application layer, i.e. the logic.*/

public class UsersController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    /*The endpoint
We need a method for this.

It should take the relevant data, pass it on to the logic layer, and return the result back to the client.
It looks like this:*/

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreateDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/user/{user.Id}", user);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        /*First, in line 1, we mark the method as [HttpPost] to say that POST requests to /users should hit this endpoint.

The method is async, to support asynchronous work. The return type is as a consequence a Task. This Task contains an ActionResult with a User inside. The ActionResult is an HTTP response type, which contains various extra data, other than what we provide.
It is just more information to the client, in case it is needed. It is good practice.

We take a UserCreationDto as the argument. This is given to the logic layer through userLogic in line 6.
The resulting User is then returned, with the method Created(), which will create an ActionResult with status code 201, the new path to this specific User (the endpoint of which we haven't made yet, but probably will), and finally the user object is also included. In our case the server only sets the ID. But in other cases, all kinds of data can be set or modified when creating an object, so generally it is polite to return the result, so the client/user can verify the result.

If anything goes wrong in the layers below, we return a status code 500. That is not very fine grained, but we do include the method of returning that error code.
A better approach is to create different custom exceptions, and catch them to then return different status codes. Maybe a ValidationException is thrown when validating the user data in the logic layer. We can then return a status code 400 indicating it was the clients fault, instead of the server.*/
    }
    /*With the Logic layer and the Data Access layer in place, we just need to create an endpoint in the UsersController, so that a client can request the data.

It looks like this:*/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
   
    /*We mark the method with [HttpGet] so that GET requests to this controller ends here.

The return value is the IEnumerable<User> wrapped in an HTTP response message.

The argument is marked as [FromQuery] to indicate that this argument should be extracted from the query parameters of the URI. The argument is of type string? indicating that it can be left out, i.e. be null.

A URI could look like:

https://localhost:7093/Users?username=roe

Indicating that we wish to filter the result by the user names which contains the text "roe".

Or if we want all users, we would use the URI:

https://localhost:7093/Users

If we later added other search parameters, e.g. age, we could have a URI like:

https://localhost:7093/Users?username=roe&age=25

Which would result in all users where the user name contains "roe" and their age is 25.*/


}