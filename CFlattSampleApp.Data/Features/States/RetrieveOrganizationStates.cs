namespace CFlattSampleApp.Data.Features.States;

public static class RetrieveOrganizationStates
{
    public class Command: IRequest<List<State>>
    {
        public int OrganizationId { get; set; }
    }

    public class Hander : IRequestHandler<Command, List<State>>
    {
        readonly CFlattSampleAppDbContext context;

        public Hander(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<State>> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = await context.Organizations.Include(a => a.States).SingleOrDefaultAsync(a => a.Id == request.OrganizationId);
            Guard.IsNotNull(organization);
            return organization.States;
        }
    }
}
