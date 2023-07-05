namespace CFlattSampleApp.Domain.Features.Organizations;

public static class RetrieveOrganizations
{
    public class Command : IRequest<List<Organization>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    public class Handler : IRequestHandler<Command, List<Organization>>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<Organization>> Handle(Command request, CancellationToken cancellationToken)
        {
            var command = new Data.Features.Organizations.RetrieveOrganizations.Command { IncludeDeleted = request.IncludeDeleted };
            var dataModel = await mediator.Send(command);
            return dataModel.ToDomainList();
        }
    }
}

