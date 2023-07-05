namespace CFlattSampleApp.Domain.Features.States;

public static class RetrieveOrganizationStates
{
    public class Command: IRequest<List<State>>
    {
        public int OrganizationId { get; set; }
    }

    public class Handler : IRequestHandler<Command, List<State>>
    {
        IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<State>> Handle(Command request, CancellationToken cancellationToken)
        {
            var command = new Data.Features.States.RetrieveOrganizationStates.Command { OrganizationId= request.OrganizationId };
            var dataModel = await mediator.Send(command, cancellationToken);
            return dataModel.ToDomainList();
        }
    }
}
