
using CommunityToolkit.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CFlattSampleApp.Data;

namespace CFlattSampleApp.Domain.Features.Users;

public static class RetrieveUser
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
            var command = new Data.Features.Users.RetrieveUser.Command() { Id = request.Id };
            var dataModel = await mediator.Send(command);
            Guard.IsNotNull(dataModel);
            return dataModel.ToDomainModel();
        }
    }
}