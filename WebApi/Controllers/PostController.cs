using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]


public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;
    private readonly IPostDao postDao;
    
        public PostController(IPostLogic postLogic)
        {
            this.postLogic = postLogic;
        }
        
        
        [HttpPost]
        public async Task<ActionResult<Post>> CreateAsync(PostCreateDto dto)
        {
            try
            {
                Post created = await postLogic.CreateAsync(dto);
                return Created($"/posts/{created.Id}", created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? username,[FromQuery] int? userId,[FromQuery] bool? completedStatus,[FromQuery] string? titleContains)
        {
           /* try
            {
                SearchPostParametersDto parameters = new(username,userId,completedStatus,titleContains);
                IEnumerable<Post> todos = await postLogic.GetAsync(parameters);
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }*/
           throw new NotImplementedException();
        }
        
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
        {
            try
            {
                await postLogic.UpdateAsync(dto);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await postLogic.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostBasicDto>> GetById([FromRoute] int id)
        {
            try
            {
                PostBasicDto result = await postLogic.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    
}