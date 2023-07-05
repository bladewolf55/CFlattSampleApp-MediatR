namespace CFlattSampleApp.Domain.Mapping;

public static class UserMapping
{
    #region "Domain"

    public static Domain.Models.User ToDomainModel(this Data.Models.User model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Deleted= model.Deleted,
            OrganizationId = model.OrganizationId
        };

    public static List<Domain.Models.User> ToDomainList(this List<Data.Models.User> list) =>
        list.Select(a => a.ToDomainModel()).ToList();

    #endregion

    #region "Data"

    public static Data.Models.User ToDataModel(this Domain.Models.User model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Deleted = model.Deleted,
            OrganizationId = model.OrganizationId
        };

    public static List<Data.Models.User> ToDataList(this List<Domain.Models.User> list) =>
        list.Select(a => a.ToDataModel()).ToList();

    #endregion
}