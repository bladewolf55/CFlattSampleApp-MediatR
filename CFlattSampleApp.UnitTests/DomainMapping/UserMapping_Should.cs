using CFlattSampleApp.Domain.Mapping;
using Xunit;

namespace CFlattSampleApp.UnitTests.DomainMapping;

public class UserMapping_Should
{
    [Fact]
    public void Map_data_user_to_domain_user()
    {
        // arrange
        Data.Models.User dataUser = new() { Id = 1, Email = "a", Name = "b", Deleted = false };
        Domain.Models.User domainUser = new() { Email = dataUser.Email, Id = dataUser.Id, Name = dataUser.Name, Deleted = dataUser.Deleted };


        // act
        var result = dataUser.ToDomainModel();

        // assert
        result.Should().BeEquivalentTo(domainUser);
    }

    [Fact]
    public void Map_data_users_to_domain_users()
    {
        // arrange
        List<Data.Models.User> dataUsers = new()
        {
            new Data.Models.User { Id = 1, Email = "a", Name = "b", Deleted = false },
            new Data.Models.User { Id = 2, Email = "a2", Name = "b2", Deleted = false }
        };

        IEnumerable<Domain.Models.User> domainUsers = new HashSet<Domain.Models.User>()
        {
            new Domain.Models.User { Id = 1, Email = "a", Name = "b", Deleted = false },
            new Domain.Models.User { Id = 2, Email = "a2", Name = "b2", Deleted = false }
        };

        // act
        var result = dataUsers.ToDomainList();

        // assert
        result.Should().BeEquivalentTo(domainUsers);
    }

    [Fact]
    public void Map_domain_user_to_data_user()
    {
        // arrange
        int organizationId = 11;
        Data.Models.User dataUser = new() { Id = 1, Email = "a", Name = "b", Deleted = false, OrganizationId = organizationId };
        Domain.Models.User domainUser = new()
        {
            Id = dataUser.Id,
            Email = dataUser.Email,
            Name = dataUser.Name,
            Deleted = dataUser.Deleted,
            OrganizationId = organizationId
        };

        // act
        var result = domainUser.ToDataModel();

        // assert
        result.Should().BeEquivalentTo(dataUser);
    }
}