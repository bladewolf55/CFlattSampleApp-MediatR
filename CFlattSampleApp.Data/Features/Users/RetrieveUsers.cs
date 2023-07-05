namespace CFlattSampleApp.Data.Features.Users;

public static class RetrieveUsers
{
    public class Command : IRequest<List<User>>
    {
        public bool IncludeDeleted { get; set; } = false;
    }

    public class Handler : IRequestHandler<Command, List<User>>
    {
        readonly CFlattSampleAppDbContext context;

        public Handler(CFlattSampleAppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> Handle(Command command, CancellationToken cancellationToken)
        {
            var model = await context.Users.Where(a => (!a.Deleted || command.IncludeDeleted) == true).ToListAsync(CancellationToken.None);
            return model;
        }
    }
}