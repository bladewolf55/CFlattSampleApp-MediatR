using Microsoft.EntityFrameworkCore.Metadata;
using CFlattSampleApp.Domain.Mapping;
using CFlattSampleApp.IntegrationTests.TestModels;
using System.Runtime.CompilerServices;
using Xunit;
namespace CFlattSampleApp.IntegrationTests;

public class UsersEndpoint_Should : WebApplicationTestBase
{
    [Fact]
    public async Task Create_a_user()
    {
        // arrange
        var count = Context.Users.Count();
        Context.ChangeTracker.Clear();
        Data.Models.User expectedDataUser = UserData.Maddie;
        expectedDataUser.OrganizationId = 1;
        Domain.Models.User submittedDomainUser = expectedDataUser.ToDomainModel();

        var json = System.Text.Json.JsonSerializer.Serialize(submittedDomainUser);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        // act
        var result = await Client.PutAsync("/users", requestContent);

        // assert
        var model = result.ValidateHttpResponseModel<Domain.Models.User>();
        model.Should().BeEquivalentTo(submittedDomainUser, options => options.Excluding(a => a.Id));
        var actualDataUser = Context.Users.Single(a => a.Id == model.Id);
        actualDataUser.Should().BeEquivalentTo(expectedDataUser, options => options.Excluding(a => a.Id));
    }

    [Fact]
    public async Task Return_a_user()
    {
        // arrange
        int userId = 1;
        var dataUser = Context.Users.Find(userId);
        var expected = dataUser?.ToDomainModel();

        // act
        var result = await Client.GetAsync("/users/1");

        // assert
        var model = result.ValidateHttpResponseModel<Domain.Models.User>();
        model.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Return_nondeleted_users_by_default()
    {
        //arrange
        int count = Context.Users.Where(a => !a.Deleted).Count();
        //act
        var result = await Client.GetAsync("/users");

        //assert
        var model = result.ValidateHttpResponseModel<List<Domain.Models.User>>();

        model.Should().HaveCount(count);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Return_users_based_on_deleted_parameter(bool includeDeleted)
    {
        int count = Context.Users.Where(a => !a.Deleted || a.Deleted == includeDeleted).Count();

        var flag = includeDeleted ? "true" : "false";
        var url = $"/users?includeDeleted={flag}";

        //act
        var result = await Client.GetAsync(url);

        //assert
        var model = result.ValidateHttpResponseModel<List<Domain.Models.User>>();
        model.Should().HaveCount(count);
    }

    [Fact]
    public async Task Update_a_user()
    {
        // arrange
        var expectedDataUser = Context.Users.First();
        Context.ChangeTracker.Clear();
        expectedDataUser.Name = "newname";
        Domain.Models.User submittedUser = expectedDataUser.ToDomainModel();

        var json = System.Text.Json.JsonSerializer.Serialize(submittedUser);
        var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

        // act
        var result = await Client.PostAsync("/users", requestContent);

        // assert
        var model = result.ValidateHttpResponseModel<Domain.Models.User>();
        model.Should().BeEquivalentTo(submittedUser);
        Context.Users.First().Should().BeEquivalentTo(expectedDataUser, options => options.Excluding(a => a.Organization));
    }

    [Fact]
    public async Task Delete_a_user()
    {
        //arrange
        var id = 1;
        var dataUser = Context.Users.Single(a => a.Id == id);
        Context.ChangeTracker.Clear();

        //act
        var result = await Client.DeleteAsync($"/users/{id}");

        //assert
        var model = result.ValidateHttpResponseModel<Domain.Models.User>();
        model.Id.Should().Be(1);
        Context.Users.Single(a => a.Id == id).Deleted.Should().BeTrue();
    }
}
