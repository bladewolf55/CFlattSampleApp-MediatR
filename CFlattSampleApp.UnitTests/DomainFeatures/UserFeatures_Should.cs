namespace CFlattSampleApp.UnitTests.DomainFeatures;

public class UserFeatures_Should
{
    readonly IMediator mediator;

    public UserFeatures_Should()
    {
        mediator = Substitute.For<IMediator>();
    }

    [Fact]
    public async Task Create_a_new_user()
    {
        // arrange
        Data.Models.User dataUser = UserData.CAUser1;
        mediator.Send(Arg.Any<Data.Features.Users.CreateUser.Command>()).Returns(dataUser);        
        Domain.Models.User domainUser = dataUser.ToDomainModel();
        var command = new Domain.Features.Users.CreateUser.Command() { User = domainUser };
        var handler = new Domain.Features.Users.CreateUser.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(domainUser);
    }

    [Fact]
    public async Task Delete_a_user_by_id()
    {
        // arrange
        var dataUser = UserData.CAUser1;
        dataUser.Deleted = true;
        mediator.Send(Arg.Is<Data.Features.Users.DeleteUser.Command>(a => a.Id == dataUser.Id)).Returns(dataUser);

        var command = new Domain.Features.Users.DeleteUser.Command { Id = dataUser.Id };
        var handler = new Domain.Features.Users.DeleteUser.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Deleted.Should().BeTrue();
    }

    [Fact]
    public async Task Return_a_user()
    {
        // arrange
        var dataUser = UserData.CAUser1;
        dataUser.Id = 1;
        mediator.Send(Arg.Is<Data.Features.Users.RetrieveUser.Command>(a => a.Id == dataUser.Id)).Returns(dataUser);
        var domainUser = dataUser.ToDomainModel();

        var command = new Domain.Features.Users.RetrieveUser.Command { Id = domainUser.Id };
        var handler = new Domain.Features.Users.RetrieveUser.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().BeEquivalentTo(domainUser);
    }

    [Fact]
    public async Task Return_users()
    {
        // arrange
        List<Data.Models.User> users = new() { UserData.CAUser1, UserData.CAUser2 };
        mediator.Send(Arg.Any<Data.Features.Users.RetrieveUsers.Command>()).Returns(users);

        var command = new Domain.Features.Users.RetrieveUsers.Command();
        var handler = new Domain.Features.Users.RetrieveUsers.Handler(mediator);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task Update_a_user()
    {
        //arrange
        var dataUser = UserData.CAUser1;
        mediator.Send(Arg.Any<Data.Features.Users.UpdateUser.Command>()).Returns(dataUser);
        dataUser.Name = "newname";
        var submittedUser = dataUser.ToDomainModel();

        var command = new Domain.Features.Users.UpdateUser.Command { User = submittedUser };
        var handler = new Domain.Features.Users.UpdateUser.Handler(mediator);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.Should().BeEquivalentTo(submittedUser);
    }
}

