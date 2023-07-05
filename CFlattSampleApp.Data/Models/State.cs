namespace CFlattSampleApp.Data.Models;

public class State
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    
    // Navigation
    public List<Organization> Organizations { get; set; } = new();
}
