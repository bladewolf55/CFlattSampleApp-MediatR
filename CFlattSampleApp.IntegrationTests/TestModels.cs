using CFlattSampleApp.Data.Models;

namespace CFlattSampleApp.IntegrationTests.TestModels;

public static class UserData
{

    public static User Maddie =>
        new()
        {
            Id = 0,
            Name = "Maddie",
            Email = "m@example.com",
        };
}

public static class OrganizationData
{
    public static Organization USOrganization =>
        new()
        {
            Id = 0,
            OrganizationType = Global.OrganizationType.External,
            Name = "US Lots-a-Pots"
        };
}
