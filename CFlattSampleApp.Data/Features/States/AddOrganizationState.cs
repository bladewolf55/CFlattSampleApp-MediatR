namespace CFlattSampleApp.Data.Features.States;

public static class AddOrganizationState
{
    public class Command: IRequest<State>
    {
        public int OrganizationId { get; set; }
        public string StateId { get; set; } = string.Empty;
    }

    public class Hander : IRequestHandler<Command, State>
    {
        readonly CFlattSampleAppDbContext context;

        public Hander(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task<State> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = await context.Organizations.Include(a => a.States).SingleOrDefaultAsync(a => a.Id == request.OrganizationId);
            Guard.IsNotNull(organization);
            var state = await context.States.SingleOrDefaultAsync(a => a.Id == request.StateId);
            Guard.IsNotNull(state);
            organization.States.Add(state);
            context.SaveChanges();
            return state;
        }
    }
}
