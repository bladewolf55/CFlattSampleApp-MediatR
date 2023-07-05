namespace CFlattSampleApp.UnitTests.TestData;

public static class StateData
{
    public static Data.Models.State CA =>
        new() { Id = "CA", Name = "California" };

    public static Data.Models.State OR =>
        new() { Id = "OR", Name = "Oregon" };
}
