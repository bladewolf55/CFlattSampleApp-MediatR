namespace CFlattSampleApp.Data.Models;

public class Organization
{
    public int Id { get; set; }
    public OrganizationType OrganizationType { get; set; }
    public string Name { get; set; } = String.Empty;
    public Boolean Deleted { get; set; }

    // Navigation
    public List<User> Users { get; set; } = new();
    public List<State> States { get; set; } = new();
}
