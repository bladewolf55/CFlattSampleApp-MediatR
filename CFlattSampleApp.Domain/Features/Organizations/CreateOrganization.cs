using CommunityToolkit.Diagnostics;

namespace CFlattSampleApp.Domain.Features.Organizations;

public static class CreateOrganization
{
    public class Command : IRequest<Domain.Models.Organization>
    {
        public Domain.Models.Organization Organization { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Domain.Models.Organization>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Domain.Models.Organization> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Organization;
            Guard.IsNotNull(model);            
            var dataModel = model.ToDataModel();
            var command = new Data.Features.Organizations.CreateOrganization.Command() { Organization = dataModel };
            dataModel = await mediator.Send(command, cancellationToken);
            return dataModel.ToDomainModel();
        }
    }
}
