namespace CFlattSampleApp.Domain.Mapping;

public static class StateMapping
{
    #region "Domain"
    public static Domain.Models.State ToDomainModel(this Data.Models.State model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name
        };

    public static List<Domain.Models.State> ToDomainList(this List<Data.Models.State> list) =>
        list.Select(a => a.ToDomainModel()).ToList();

    #endregion

    #region "Data"
    public static Data.Models.State ToDataModel(this Domain.Models.State model) =>
    new()
    {
        Id = model.Id,
        Name = model.Name
    };

    public static List<Data.Models.State> ToDataList(this List<Domain.Models.State> list) =>
        list.Select(a => a.ToDataModel()).ToList();
    #endregion
}
