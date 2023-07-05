namespace CFlattSampleApp.Domain.Features.Users;

public static class DeleteUser
{
    public class Command : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, User>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<User> Handle(Command request, CancellationToken cancellationToken)
        {
            var command = new Data.Features.Users.DeleteUser.Command {  Id = request.Id };
            var dataModel = await mediator.Send(command);
            return dataModel.ToDomainModel();
        }
    }
}
