namespace CFlattSampleApp.Data.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Deleted { get; set; } = false;

    // Navigation
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
}