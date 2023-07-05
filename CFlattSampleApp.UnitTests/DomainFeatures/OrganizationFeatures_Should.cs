namespace CFlattSampleApp.UnitTests.DomainFeatures;

public class OrganizationFeatures_Should
{
    readonly IMediator mediator;

    public OrganizationFeatures_Should()
    {
        mediator = Substitute.For<IMediator>();
    }

    [Fact]
    public async Task Create_a_new_organization()
    {
        // arrange
        Data.Models.Organization dataOrganization = OrganizationData.CAOrganization;
        mediator.Send(Arg.Any<Data.Features.Organizations.CreateOrganization.Command>()).Returns(Task.FromResult(dataOrganization));    
        Domain.Models.Organization domainOrganization = dataOrganization.ToDomainModel();
        var command = new Domain.Features.Organizations.CreateOrganization.Command() { Organization = domainOrganization };
        var handler = new Domain.Features.Organizations.CreateOrganization.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(domainOrganization);
    }

    [Fact]
    public async Task Delete_an_organization_by_id()
    {
        // arrange
        var dataOrganization = OrganizationData.CAOrganization;
        dataOrganization.Deleted = true;
        mediator.Send(Arg.Is<Data.Features.Organizations.DeleteOrganization.Command>(a => a.Id == dataOrganization.Id)).Returns(dataOrganization);

        var command = new Domain.Features.Organizations.DeleteOrganization.Command { Id = dataOrganization.Id };
        var handler = new Domain.Features.Organizations.DeleteOrganization.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Deleted.Should().BeTrue();
    }

    [Fact]
    public async Task Return_an_organization()
    {
        // arrange
        var dataOrganization = OrganizationData.CAOrganization;
        dataOrganization.Id = 1;
        mediator.Send(Arg.Is<Data.Features.Organizations.RetrieveOrganization.Command>(a => a.Id == dataOrganization.Id)).Returns(dataOrganization);
        var domainOrganization = dataOrganization.ToDomainModel();

        var command = new Domain.Features.Organizations.RetrieveOrganization.Command { Id = domainOrganization.Id };
        var handler = new Domain.Features.Organizations.RetrieveOrganization.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(domainOrganization);
    }

    [Fact]
    public async Task Return_organizations()
    {
        // arrange
        List<Data.Models.Organization> organizations = new() { OrganizationData.CAOrganization, OrganizationData.CFlattOrganization };
        mediator.Send(Arg.Any<Data.Features.Organizations.RetrieveOrganizations.Command>()).Returns(organizations);

        var command = new Domain.Features.Organizations.RetrieveOrganizations.Command();
        var handler = new Domain.Features.Organizations.RetrieveOrganizations.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task Update_an_organization()
    {
        //arrange
        var dataOrganization = OrganizationData.CAOrganization;
        mediator.Send(Arg.Any<Data.Features.Organizations.UpdateOrganization.Command>()).Returns(dataOrganization);
        dataOrganization.Name = "newname";
        var submittedOrganization = dataOrganization.ToDomainModel();

        var command = new Domain.Features.Organizations.UpdateOrganization.Command { Organization = submittedOrganization };
        var handler = new Domain.Features.Organizations.UpdateOrganization.Handler(mediator);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.Should().BeEquivalentTo(submittedOrganization);
    }
}

