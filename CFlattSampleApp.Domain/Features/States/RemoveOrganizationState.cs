using CommunityToolkit.Diagnostics;

namespace CFlattSampleApp.Domain.Features.States;

public static class RemoveOrganizationState
{
    public class Command: IRequest
    {
        public int OrganizationId { get; set; }
        public string StateId { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Command>
    {
        IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            Guard.IsNotNullOrEmpty(request.StateId);
            var command = new Data.Features.States.RemoveOrganizationState.Command { OrganizationId = request.OrganizationId, StateId = request.StateId };
            await mediator.Send(command, cancellationToken);
            return;
        }

    }
}
