using CFlattSampleApp.Domain.Features.Users;

namespace CFlattSampleApp.WebApi.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly IMediator mediator;

    public UsersController(ILogger<UsersController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet("users/{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        try
        {
            var user = await mediator.Send(new RetrieveUser.Command { Id = id });
            if (user == null)
                throw new KeyNotFoundException();
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "User not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to get user");
            return BadRequest();
        }
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<User>>> Get(bool includeDeleted)
    {
        var users = await mediator.Send(new RetrieveUsers.Command { IncludeDeleted = includeDeleted });
        return Ok(users);
    }

    [HttpPut("users")]
    public async Task<ActionResult<User>> Put(User model)
    {
        try
        {
            var user = await mediator.Send(new CreateUser.Command() { User = model });
            return Ok(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to add user");
            return BadRequest();
        }
    }

    [HttpPost("users")]
    public async Task<ActionResult<User>> Post(User model)
    {
        try
        {
            var user = await mediator.Send(new UpdateUser.Command() { User = model });
            if (user == null)
                throw new KeyNotFoundException();
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "User not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to update user");
            return BadRequest();
        }
    }

    [HttpDelete("users/{id}")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        try
        {
            var user = await mediator.Send(new DeleteUser.Command() { Id = id });
            if (user == null)
                throw new KeyNotFoundException();
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "User not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to delete user");
            return BadRequest();
        }
    }
}