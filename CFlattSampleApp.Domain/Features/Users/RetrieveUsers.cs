namespace CFlattSampleApp.Domain.Features.Users;

public static class RetrieveUsers
{
    public class Command : IRequest<List<User>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    public class Handler : IRequestHandler<Command, List<User>>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<List<User>> Handle(Command request, CancellationToken cancellationToken)
        {
            var command = new Data.Features.Users.RetrieveUsers.Command { IncludeDeleted = request.IncludeDeleted };
            var dataModel = await mediator.Send(command);
            return dataModel.ToDomainList();
        }
    }
}

