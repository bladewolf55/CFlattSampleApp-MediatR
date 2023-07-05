using CommunityToolkit.Diagnostics;

namespace CFlattSampleApp.Domain.Features.States;

public static class AddOrganizationState
{
    public class Command: IRequest<State>
    {
        public int OrganizationId { get; set; }
        public string StateId { get; set; } = string.Empty;
    }

    public class Hander : IRequestHandler<Command, State>
    {
        IMediator mediator;

        public Hander(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<State> Handle(Command request, CancellationToken cancellationToken)
        {
            Guard.IsNotNullOrEmpty(request.StateId);

            var command = new Data.Features.States.AddOrganizationState.Command { OrganizationId = request.OrganizationId, StateId = request.StateId };
            var dataModel = await mediator.Send(command, cancellationToken);
            return dataModel.ToDomainModel();
        }
    }
}
