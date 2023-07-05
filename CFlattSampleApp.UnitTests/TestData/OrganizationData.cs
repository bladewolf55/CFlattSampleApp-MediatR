namespace CFlattSampleApp.UnitTests.TestData;

/// <summary>
/// Ids are unique
/// </summary>
public static class OrganizationData
{
    public static Data.Models.Organization CFlattOrganization =>
        new()
        {
            Id = 1,
            OrganizationType = OrganizationType.Internal,
            Name = "CFlatt",
        };

    public static Data.Models.Organization CAOrganization =>
        new()
        {
            Id = 1,
            OrganizationType = OrganizationType.Internal,
            Name = "California Cookin",
        };
}
