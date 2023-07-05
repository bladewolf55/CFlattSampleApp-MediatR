namespace CFlattSampleApp.Data.Features.Organizations;

public static class RetrieveOrganizations
{
    public class Command : IRequest<List<Organization>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    public class Handler : IRequestHandler<Command, List<Organization>>
    {
        readonly CFlattSampleAppDbContext context;

        public Handler(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Organization>> Handle(Command command, CancellationToken cancellationToken)
        {
            var model = await context.Organizations.Where(a => (!a.Deleted || command.IncludeDeleted) == true).ToListAsync(CancellationToken.None);
            return model;
        }
    }
}
