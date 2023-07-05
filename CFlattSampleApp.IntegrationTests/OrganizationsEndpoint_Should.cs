using CFlattSampleApp.Domain.Mapping;
using CFlattSampleApp.IntegrationTests.TestModels;

namespace CFlattSampleApp.IntegrationTests;

public class OrganizationsEndpoint_Should : WebApplicationTestBase
{
    [Fact]
    public async Task Create_a_organization()
    {
        // arrange
        var count = Context.Organizations.Count();
        Context.ChangeTracker.Clear();
        Data.Models.Organization expectedDataOrganization = OrganizationData.USOrganization;
        Domain.Models.Organization submittedDomainOrganization = expectedDataOrganization.ToDomainModel();

        var json = System.Text.Json.JsonSerializer.Serialize(submittedDomainOrganization);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        // act
        var result = await Client.PutAsync("/organizations", requestContent);

        // assert
        var model = result.ValidateHttpResponseModel<Domain.Models.Organization>();
        model.Should().BeEquivalentTo(submittedDomainOrganization, options => options.Excluding(a => a.Id));
        var actualDataOrganization = Context.Organizations.Single(a => a.Id == model.Id);
        actualDataOrganization.Should().BeEquivalentTo(expectedDataOrganization, options => options.Excluding(a => a.Id));
    }

    [Fact]
    public async Task Return_a_organization()
    {
        // arrange
        int organizationId = 1;
        var dataOrganization = Context.Organizations.Find(organizationId);
        var expected = dataOrganization?.ToDomainModel();

        // act
        var result = await Client.GetAsync("/organizations/1");

        // assert
        var model = result.ValidateHttpResponseModel<Domain.Models.Organization>();
        model.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Return_nondeleted_organizations_by_default()
    {
        //arrange
        int count = Context.Organizations.Where(a => !a.Deleted).Count();

        //act
        var result = await Client.GetAsync("/organizations");

        //assert
        var model = result.ValidateHttpResponseModel<List<Domain.Models.Organization>>();

        model.Should().HaveCount(count);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Return_organizations_based_on_deleted_parameter(bool includeDeleted)
    {
        int count = Context.Organizations.Where(a => !a.Deleted || a.Deleted == includeDeleted).Count();

        var flag = includeDeleted ? "true" : "false";
        var url = $"/organizations?includeDeleted={flag}";

        //act
        var result = await Client.GetAsync(url);

        //assert
        var model = result.ValidateHttpResponseModel<List<Domain.Models.Organization>>();
        model.Should().HaveCount(count);
    }

    [Fact]
    public async Task Update_a_organization()
    {
        // arrange
        var expectedDataOrganization = Context.Organizations.First();
        Context.ChangeTracker.Clear();
        expectedDataOrganization.Name = "newname";
        Domain.Models.Organization submittedOrganization = expectedDataOrganization.ToDomainModel();

        var json = System.Text.Json.JsonSerializer.Serialize(submittedOrganization);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        // act
        var result = await Client.PostAsync("/organizations", requestContent);

        // assert
        var model = result.ValidateHttpResponseModel<Domain.Models.Organization>();
        model.Should().BeEquivalentTo(submittedOrganization);
        Context.Organizations.First().Should().BeEquivalentTo(expectedDataOrganization, options => options
            .Excluding(a => a.Users)
            .Excluding(a => a.States)
            );
    }

    [Fact]
    public async Task Delete_a_organization()
    {
        //arrange
        var id = 1;
        var dataOrganization = Context.Organizations.Single(a => a.Id == id);
        Context.ChangeTracker.Clear();

        //act
        var result = await Client.DeleteAsync($"/organizations/{id}");

        //assert
        var model = result.ValidateHttpResponseModel<Domain.Models.Organization>();
        model.Id.Should().Be(1);
        Context.Organizations.Single(a => a.Id == id).Deleted.Should().BeTrue();
    }
}
