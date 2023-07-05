using CommunityToolkit.Diagnostics;
using CFlattSampleApp.Data;

namespace CFlattSampleApp.Domain.Features.Users;

public static class CreateUser
{
    public class Command : IRequest<Domain.Models.User>
    {
        public Domain.Models.User User { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Domain.Models.User>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Domain.Models.User> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.User;
            Guard.IsNotNull(model);            
            var dataModel = model.ToDataModel();
            var command = new Data.Features.Users.CreateUser.Command() { User = dataModel };
            dataModel = await mediator.Send(command, cancellationToken);
            return dataModel.ToDomainModel();
        }
    }
}
