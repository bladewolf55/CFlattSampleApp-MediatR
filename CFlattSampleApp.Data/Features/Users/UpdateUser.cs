namespace CFlattSampleApp.Data.Features.Users;

public static class UpdateUser
{
    public class Command : IRequest<User>
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
            var submittedModel = request.User;
            Guard.IsNotNull(submittedModel);
            context.Users.Update(submittedModel);
            var currentModel = context.Users.Find(request.User.Id);
            Guard.IsNotNull(currentModel);
            currentModel = submittedModel;
            context.Users.Entry(submittedModel).State = EntityState.Modified;
            _ = await context.SaveChangesAsync(CancellationToken.None);
            return currentModel;
        }
    }
}
