namespace CFlattSampleApp.Data.Features.Organizations;

public static class RetrieveOrganization
{
    public class Command : IRequest<Organization>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Organization>
    {
        readonly CFlattSampleAppDbContext context;

        public Handler(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task<Organization> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = await context.Organizations.FindAsync(request.Id);
            Guard.IsNotNull(model);
            return model;
        }
    }
}
