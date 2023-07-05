using CFlattSampleApp.Domain.Mapping;

namespace CFlattSampleApp.UnitTests.DomainMapping;

public class OrganizationMapping_Should
{
    [Fact]
    public void Map_data_organization_to_domain_organization()
    {
        // arrange
        List<Data.Models.User> dataUsers = new() { new(), new() };
        Data.Models.Organization dataOrganization = new() { Id = 1, Name = "b", Users = dataUsers };
        Domain.Models.Organization domainOrganization = new() { Id = dataOrganization.Id, Name = dataOrganization.Name };

        // act
        var result = dataOrganization.ToDomainModel();

        // assert
        result.Should().BeEquivalentTo(domainOrganization);
    }

    [Fact]
    public void Map_domain_organization_to_data_organization()
    {
        // arrange
        Data.Models.Organization dataOrganization = new() { Id = 1, Name = "b" };
        Domain.Models.Organization domainOrganization = new() { Id = dataOrganization.Id, Name = dataOrganization.Name };

        // act
        var result = domainOrganization.ToDataModel();

        // assert
        using (new FluentAssertions.Execution.AssertionScope())
        {
            result.Id.Should().Be(dataOrganization.Id);
            result.Name.Should().Be(dataOrganization.Name);
        }
    }
}