using CommunityToolkit.Diagnostics;

namespace CFlattSampleApp.Domain.Features.Organizations;

public static class UpdateOrganization
{
    public class Command : IRequest<Organization>
    {
        public Organization Organization { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Organization>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Organization> Handle(Command request, CancellationToken cancellationToken)
        {
            var submittedModel = request.Organization;
            Guard.IsNotNull(submittedModel);
            var command = new Data.Features.Organizations.UpdateOrganization.Command { Organization = submittedModel.ToDataModel() };
            var dataModel = await mediator.Send(command, cancellationToken);
            return dataModel.ToDomainModel();
        }
    }


}
