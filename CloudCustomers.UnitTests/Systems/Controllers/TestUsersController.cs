using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockUsersSservice = new Mock<IUsersService>();
        mockUsersSservice
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersSservice.Object);

        // Act
        var result = (OkObjectResult) await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUsersServiceExactlyOnce()
    {
        // Arrange
        var mockUsersSservice = new Mock<IUsersService>();
        mockUsersSservice
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersSservice.Object);

        // Act
        var result = await sut.Get();

        // Assert
        mockUsersSservice.Verify(service => service.GetAllUsers(), Times.Once());
    }
}