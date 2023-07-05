namespace CFlattSampleApp.Domain.Features.Organizations;

public static class DeleteOrganization
{
    public class Command : IRequest<Organization>
    {
        public int Id { get; set; }
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
            var command = new Data.Features.Organizations.DeleteOrganization.Command {  Id = request.Id };
            var dataModel = await mediator.Send(command);
            return dataModel.ToDomainModel();
        }
    }
}
