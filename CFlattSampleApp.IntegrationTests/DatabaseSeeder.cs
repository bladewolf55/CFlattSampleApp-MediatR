using Microsoft.EntityFrameworkCore;
using CFlattSampleApp.Data.Models;

namespace CFlattSampleApp.IntegrationTests;

public class DatabaseSeeder
{
    CFlattSampleAppDbContext context = null!;

    public void Seed(CFlattSampleAppDbContext context)
    {
        this.context = context;
        AddOrganizations();
        AssignStatesToOrganizations();
        AddUsers();
    }

    private void AddOrganizations()
    {
        List<Organization> organizations = new() {
            new Organization { OrganizationType = Global.OrganizationType.Internal, Name = "CFlatt" },
            new Organization { OrganizationType = Global.OrganizationType.External, Name = "California Cookin" },
            new Organization { OrganizationType = Global.OrganizationType.External, Name = "US Lots-a-Pots" },
            new Organization { OrganizationType = Global.OrganizationType.External, Name = "Old Spoons (Deleted)", Deleted = true },
        };
        context.Organizations.AddRange(organizations);
        context.SaveChanges();
    }

    private void AssignStatesToOrganizations()
    {
        context.Organizations.Single(a => a.Id == 1).States = context.States.ToList();
        context.Organizations.Single(a => a.Id == 2).States = context.States.Where(a => a.Id == "CA").ToList();
        context.Organizations.Single(a => a.Id == 3).States = context.States.ToList();
        context.SaveChanges();
    }

    private void AddUsers()
    {
        List<User> users = new() {
            new User { Name = "Agnes", Email = "a@example.com", OrganizationId = 1 },
            new User { Name = "Bizhan", Email = "b@example.com", OrganizationId = 1 },
            new User { Name = "Clarice", Email = "c@example.com", OrganizationId = 2 },
            new User { Name = "Dre", Email = "d@example.com", OrganizationId = 2 },
            new User { Name = "Eustice", Email = "e@example.com", OrganizationId = 3 },
            new User { Name = "Francoise", Email = "f@example.com", OrganizationId = 3 },
            new User { Name = "Gregor", Email = "g@example.com", OrganizationId = 3, Deleted = true },
        };
        context.Users.AddRange(users);
        context.SaveChanges();
    }

}
