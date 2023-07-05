
namespace CFlattSampleApp.Domain.Mapping;

public static class OrganizationMapping
{
    #region "Domain"

    public static Domain.Models.Organization ToDomainModel(this Data.Models.Organization model) =>
        new()
        {
            Id = model.Id,
            OrganizationType = model.OrganizationType,
            Name = model.Name,
            Deleted = model.Deleted,
        };

    public static List<Domain.Models.Organization> ToDomainList(this List<Data.Models.Organization> list) =>
        list.Select(a => a.ToDomainModel()).ToList();
    #endregion

    #region "Data"
    public static Data.Models.Organization ToDataModel(this Domain.Models.Organization model) =>
        new()
        {
            Id = model.Id,
            OrganizationType = model.OrganizationType,
            Name = model.Name,
            Deleted = model.Deleted
        };

    public static List<Data.Models.Organization> ToDataList(this List<Domain.Models.Organization> list) =>
        list.Select(a => a.ToDataModel()).ToList();
    #endregion
}
