using CFlattSampleApp.Domain.Features.Organizations;

namespace CFlattSampleApp.WebApi.Controllers;

[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly ILogger<OrganizationsController> logger;
    private readonly IMediator mediator;

    public OrganizationsController(ILogger<OrganizationsController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet("organizations/{id}")]
    public async Task<ActionResult<Organization>> Get(int id)
    {
        try
        {
            var organization = await mediator.Send(new RetrieveOrganization.Command { Id = id });
            if (organization == null)
                throw new KeyNotFoundException();
            return Ok(organization);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "Organization not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to get organization");
            return BadRequest();
        }
    }

    [HttpGet("organizations")]
    public async Task<ActionResult<List<Organization>>> Get(bool includeDeleted)
    {
        var organizations = await mediator.Send(new RetrieveOrganizations.Command { IncludeDeleted = includeDeleted });
        return Ok(organizations);
    }

    [HttpPut("organizations")]
    public async Task<ActionResult<Organization>> Put(Organization model)
    {
        try
        {
            var organization = await mediator.Send(new CreateOrganization.Command() { Organization = model });
            return Ok(organization);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to add organization");
            return BadRequest();
        }
    }

    [HttpPost("organizations")]
    public async Task<ActionResult<Organization>> Post(Organization model)
    {
        try
        {
            var organization = await mediator.Send(new UpdateOrganization.Command() { Organization = model });
            if (organization == null)
                throw new KeyNotFoundException();
            return Ok(organization);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "Organization not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to update organization");
            return BadRequest();
        }
    }

    [HttpDelete("organizations/{id}")]
    public async Task<ActionResult<Organization>> Delete(int id)
    {
        try
        {
            var organization = await mediator.Send(new DeleteOrganization.Command() { Id = id });
            if (organization == null)
                throw new KeyNotFoundException();
            return Ok(organization);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "Organization not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to delete organization");
            return BadRequest();
        }
    }
}