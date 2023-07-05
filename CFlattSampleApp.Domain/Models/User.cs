namespace CFlattSampleApp.Domain.Models;

public class User
{
    public string Email { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Deleted { get; set; } = false;

    // Navigation
    public int OrganizationId { get; set; }

}