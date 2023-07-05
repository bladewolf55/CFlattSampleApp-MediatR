namespace CFlattSampleApp.Data.Features.Users;

public class CreateUser
{
    public class Command: IRequest<User>
    {
        public User User { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, User>
    {
        readonly CFlattSampleAppDbContext context;

        public Handler(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task<User> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.User;
            Guard.IsNotNull(model);
            Guard.IsNull(context.Users.Find(model.Id));
            context.Users.Add(model);
            _ = await context.SaveChangesAsync(CancellationToken.None);
            return model;
        }
    }
}
