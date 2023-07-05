namespace CFlattSampleApp.UnitTests.TestData;

/// <summary>
/// Id is unique
/// OrganizationId is always 1
/// Organization is always null
/// </summary>
public static class UserData
{
    public static Data.Models.User CAUser1 =>
        new()
        {
            Id = 1,
            Name = "CA User 1",
            Email = "causer1@example.com",
            Deleted = false,
            OrganizationId= 1,
        };

    public static Data.Models.User CAUser2 =>
        new()
        {
            Id = 2,
            Name = "CA User 2",
            Email = "causer2@example.com",
            Deleted = false,
            OrganizationId = 1,
        };
}
