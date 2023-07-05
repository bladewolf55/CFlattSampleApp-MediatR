
namespace CFlattSampleApp.Domain.Models;

public class Organization
{
    public int Id { get; set; }
    public OrganizationType OrganizationType { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Deleted { get; set; }

}
