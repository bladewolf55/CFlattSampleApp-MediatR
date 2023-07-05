using Azure.Core;
using System.Threading.Tasks;
using Xunit;
namespace CFlattSampleApp.UnitTests.DomainFeatures;

public class OrganizationStateFeatures_Should
{
    readonly IMediator mediator;

    public OrganizationStateFeatures_Should()
    {
        mediator = Substitute.For<IMediator>();
    }

    [Fact]
    public async Task Get_an_organizations_states()
    {
        // arrange
        List<Data.Models.State> states = new() { StateData.CA, StateData.OR };
        mediator.Send(Arg.Any<Data.Features.States.RetrieveOrganizationStates.Command>()).Returns(states);

        var command = new Domain.Features.States.RetrieveOrganizationStates.Command { OrganizationId = 1 };
        var handler = new Domain.Features.States.RetrieveOrganizationStates.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(states.ToDomainList());
    }

    [Fact]
    public async Task Add_a_state_to_an_organization()
    {
        // arrange
        var dataState = StateData.CA;
        mediator.Send(Arg.Any<Data.Features.States.AddOrganizationState.Command>()).Returns(dataState);

        var command = new Domain.Features.States.AddOrganizationState.Command { OrganizationId = 1, StateId = dataState.Id };
        var handler = new Domain.Features.States.AddOrganizationState.Hander(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(dataState.ToDomainModel());
    }

    [Fact]
    public async Task Remove_a_state_from_an_organization()
    {
        // arrange
        var organizationId = 1;
        var stateId = "CA";

        var command = new Domain.Features.States.RemoveOrganizationState.Command { OrganizationId = organizationId, StateId = stateId };
        var handler = new Domain.Features.States.RemoveOrganizationState.Handler(mediator);

        // act
        await handler.Handle(command, CancellationToken.None);

        // assert
        await mediator.Received().Send(
            Arg.Is<Data.Features.States.RemoveOrganizationState.Command>(a => a.OrganizationId == organizationId && a.StateId == stateId), 
            CancellationToken.None);
    }

}
