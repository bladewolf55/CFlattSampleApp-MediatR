using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CFlattSampleApp.Data.Features.States;

public static class RemoveOrganizationState
{
    public class Command : IRequest
    {
        public int OrganizationId { get; set; }
        public string StateId { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Command>
    {
        readonly CFlattSampleAppDbContext context;

        public Handler(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            Guard.IsNotNullOrEmpty(request.StateId);

            var organization = await context.Organizations.Include(a => a.States).SingleOrDefaultAsync(a => a.Id == request.OrganizationId, cancellationToken);
            Guard.IsNotNull(organization);
            var state = organization.States.SingleOrDefault(a => a.Id == request.StateId);
            if (state != null)
            {
                organization.States.Remove(state);
                await  context.SaveChangesAsync(cancellationToken);
            }
            return;
        }
    }
}
