using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
namespace CFlattSampleApp.IntegrationTests;

public class OrganizationStatesEndpoint_Should : WebApplicationTestBase
{
    [Fact]
    public async void Return_an_organizations_states()
    {
        // arrange
        var count = Context.States.Count();
        Context.ChangeTracker.Clear();

        // act
        var result = await Client.GetAsync("/organizations/1/states");

        // assert
        var model = result.ValidateHttpResponseModel<List<Domain.Models.State>>();
        model.Should().HaveCount(count);
    }

    [Fact]
    public async void Add_a_state_to_an_organization()
    {
        // arrange
        // Cal Fire
        var organizationId = 2;
        var organization = Context.Organizations.Include(a => a.States).Single(a => a.Id == organizationId);
        var count = organization.States.Count;
        Context.ChangeTracker.Clear();
        var stateId = "OR";

        // act
        var result = await Client.PostAsync($"/organizations/{organizationId}/states?stateId={stateId}", null);

        // assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        organization = Context.Organizations.Include(a => a.States).Single(a => a.Id == organizationId);
        organization.States.Should().HaveCount(count + 1);        
    }

    [Fact]
    public async Task Remove_a_state_from_an_organization()
    {
        // arrange
        // CFlatt
        var organizationId = 1;
        var organization = Context.Organizations.Include(a => a.States).Single(a => a.Id == organizationId);
        var count = organization.States.Count;
        var stateId = organization.States.First().Id;
        Context.ChangeTracker.Clear();

        // act
        var result = await Client.DeleteAsync($"/organizations/{organizationId}/states?stateId={stateId}");

        // assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        organization = Context.Organizations.Include(a => a.States).Single(a => a.Id == organizationId);
        organization.States.Should().HaveCount(count - 1);

    }
}
