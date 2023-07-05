
using CommunityToolkit.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CFlattSampleApp.Data;

namespace CFlattSampleApp.Domain.Features.Organizations;

public static class RetrieveOrganization
{
    public class Command : IRequest<Organization>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Organization>
    {
        readonly IMediator mediator;

        public Handler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Organization> Handle(Command request, CancellationToken cancellationToken)
        {
            var command = new Data.Features.Organizations.RetrieveOrganization.Command() { Id = request.Id };
            var dataModel = await mediator.Send(command);
            Guard.IsNotNull(dataModel);
            return dataModel.ToDomainModel();
        }
    }
}