using CFlattSampleApp.WebApi;
using CFlattSampleApp.Domain;
using CFlattSampleApp.Domain.Models;
using CFlattSampleApp.WebApi.Controllers;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CFlattSampleApp.Domain.Features;

namespace CFlattSampleApp.UnitTests.DomainMapping;

public class UsersController_Should
{
    ILogger<UsersController> logger;
    UsersController userController;
    IMediator mediator;

    public UsersController_Should()
    {
        logger = Substitute.For<ILogger<UsersController>>();
        mediator = Substitute.For<IMediator>();
        userController = new(logger, mediator);
    }

    [Fact]
    public async Task Return_BadRequest_if_user_not_found_by_email()
    {
        // act
        var result = await userController.Get(1);

        // assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Return_BadRequest_if_user_not_found_by_id()
    {
        // act
        var result = await userController.Get(0);

        // assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Return_ok_and_user_if_user_returned_by_id()
    {
        // arrange
        User user = new User { Id = 1, Email = "a", Name = "b" };
        mediator.Send(Arg.Any<IRequest<User>>()).Returns(Task.FromResult(user));

        // act
        var result = await userController.Get(user.Id);

        // assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Add_a_new_user()
    {
        // arrange
        User user = new User { Id = 1 };
        mediator.Send(Arg.Any<IRequest<User>>()).Returns(Task.FromResult(user));

        // act
        var result = await userController.Put(user);

        // assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Delete_an_existing_user()
    {
        // arrange
        User user = new User { Id = 1, Deleted = true };
        mediator.Send(Arg.Any<IRequest<User>>()).Returns(Task.FromResult(user));

        // act
        var result = await userController.Delete(user.Id);

        // assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Return_NotFound_if_no_user_to_delete()
    {
        //act 
        var result = await userController.Delete(0);

        //assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Update_a_user()
    {
        //arrange
        var user = new User { Id = 1, Email = "a", Name = "b" };
        mediator.Send(Arg.Any<IRequest<User>>()).Returns(Task.FromResult(user));

        //act
        var result = await userController.Post(user);

        //assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task Return_NotFound_if_user_does_not_exist()
    {
        //act
        var result = await userController.Post(new User { Id = 0 });

        //assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Retrieve_all_users()
    {
        //arrange
        var userList = new List<User>()
        {
            new User { Id = 1 },
            new User { Id = 2 },
            new User { Id = 3 }
        };
        mediator.Send(Arg.Any<IRequest<List<User>>>()).Returns(Task.FromResult(userList));

        //act
        var result = await userController.Get(includeDeleted: false);

        //assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeEquivalentTo(userList);
    }

}
