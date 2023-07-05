using CFlattSampleApp.Data.Features.States;
using CFlattSampleApp.Domain.Features.Organizations;

namespace CFlattSampleApp.WebApi.Controllers;

[ApiController]
public class OrganizationStatesController : ControllerBase
{
    private readonly ILogger<OrganizationsController> logger;
    private readonly IMediator mediator;

    public OrganizationStatesController(ILogger<OrganizationsController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet("organizations/{id}/states")]
    public async Task<ActionResult<List<State>>> Get(int id)
    {
        try
        {
            var states = await mediator.Send(new Domain.Features.States.RetrieveOrganizationStates.Command { OrganizationId = id });
            if (states == null)
                throw new KeyNotFoundException();
            return Ok(states);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "States not found");
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to get states");
            return BadRequest();
        }
    }

    [HttpPost("organizations/{id}/states")]
    public async Task<ActionResult<State>> Post(int id, string stateId)
    {
        try
        {
            var command = new Domain.Features.States.AddOrganizationState.Command { OrganizationId = id, StateId = stateId };
            var state = await mediator.Send(command);
            return Ok(state);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to add state");
            return BadRequest();
        }
    }

    [HttpDelete("organizations/{id}/states")]
    public async Task<ActionResult> Delete(int id, string stateId)
    {
        try
        {
            var command = new Domain.Features.States.RemoveOrganizationState.Command { OrganizationId = id, StateId = stateId };
            await mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to add state");
            return BadRequest();
        }
    }
}