namespace CFlattSampleApp.Data.Features.Organizations;

public class CreateOrganization
{
    public class Command : IRequest<Organization>
    {
        public Organization Organization { get; set; } = null!;
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
            var model = request.Organization;
            Guard.IsNotNull(model);
            Guard.IsNull(context.Organizations.Find(model.Id));
            context.Organizations.Add(model);
            _ = await context.SaveChangesAsync(CancellationToken.None);
            return model;
        }
    }
}
