namespace CFlattSampleApp.Data.Features.Organizations;

public static class UpdateOrganization
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
            var submittedModel = request.Organization;
            Guard.IsNotNull(submittedModel);
            context.Organizations.Update(submittedModel);
            var currentModel = context.Organizations.Find(request.Organization.Id);
            Guard.IsNotNull(currentModel);
            currentModel = submittedModel;
            context.Organizations.Entry(submittedModel).State = EntityState.Modified;
            _ = await context.SaveChangesAsync(CancellationToken.None);
            return currentModel;
        }
    }


}
