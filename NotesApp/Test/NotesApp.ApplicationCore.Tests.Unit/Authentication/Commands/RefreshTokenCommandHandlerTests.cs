using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace NotesApp.ApplicationCore.Tests.Unit.Authentication.Commands;

public class RefreshTokenCommandHandlerTests
{
    private readonly RefreshTokenCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IAuthService _authService = Substitute.For<IAuthService>();
    private readonly CancellationToken _cancellationToken = new();
    private readonly RefreshTokenCommand _refreshTokenCommand;
    
    public RefreshTokenCommandHandlerTests()
    {
        _sut = new RefreshTokenCommandHandler(_unitOfWork, _authService);
        _refreshTokenCommand = new RefreshTokenCommand("refresh token");
    }

    [Fact]
    public async Task Handler_ShouldReturnInvalidCredentialsException_WhenRefreshTokenIsNull()
    {
        // Arrange
        var refreshTokenCommand = new RefreshTokenCommand(null);
        
        var expectedException = new InvalidCredentialException("Invalid Credentials");
        //  Act
        var result = await  _sut.Handle(refreshTokenCommand, _cancellationToken);
        
        Exception exceptionResult = new Exception(); 
        result.Match<bool>(obj => true,  exception =>
        {
            exceptionResult = exception;
            return true; 
        });
        
        // Assert
        result.IsFaulted.Should().BeTrue();
        exceptionResult.Should().BeEquivalentTo(expectedException);
    }
    
    
    [Fact]
    public async Task Handler_ShouldReturnInvalidCredentialsException_WhenUserDoesNotExist()
    {
        // Arrange
        var expectedException = new InvalidCredentialException("Invalid Credentials");
        _unitOfWork.Users.GetUserByRefreshToken(Arg.Any<string>()).ReturnsNull();
        //  Act
        var result = await  _sut.Handle(_refreshTokenCommand, _cancellationToken);
        
        Exception exceptionResult = new Exception(); 
        result.Match<bool>(obj => true,  exception =>
        {
            exceptionResult = exception;
            return true; 
        });
        
        // Assert
        result.IsFaulted.Should().BeTrue();
        exceptionResult.Should().BeEquivalentTo(expectedException);
    }
    
    [Fact]
    public async Task Handler_ShouldReturnInvalidCredentialsException_WhenRefreshTokenIsExpired()
    {
        // Arrange
        var user = new User()
        {
            TokenExpires = DateTime.Now.AddDays(-1)
        };
        
        _unitOfWork.Users.GetUserByRefreshToken(Arg.Any<string>()).Returns(user);

        var expectedException = new InvalidCredentialException("Invalid Credentials");
        
        //  Act
        var result = await  _sut.Handle(_refreshTokenCommand, _cancellationToken);
        
        Exception exceptionResult = new Exception(); 
        result.Match<bool>(obj => true,  exception =>
        {
            exceptionResult = exception;
            return true; 
        });
        
        // Assert
        result.IsFaulted.Should().BeTrue();
        exceptionResult.Should().BeEquivalentTo(expectedException);
    }
    
    [Fact]
    public async Task Handler_ShouldReturnNewRefreshToken_WhenRefreshTokenIsValid()
    {
        // Arrange
        var user = new User()
        {
            TokenExpires = DateTime.Now.AddDays(1)
        };
        
        var created = DateTime.Now;
        var expires = DateTime.Now.AddDays(1);
        
        var fullToken = new JwtToken()
        {
            Created = created,
            RefreshToken = "refreshToken",
            Expires = expires,
            Token = "token"
        };

        var expectedFullToken = new JwtToken()
        {
            Created = created,
            RefreshToken = "refreshToken",
            Expires = expires,
            Token = "token"
        };
        
        _unitOfWork.Users.GetUserByRefreshToken(Arg.Any<string>()).Returns(user);

        _authService.SetTokenAndRefreshToken(user).Returns(fullToken);
        
        //  Act
        var result = await  _sut.Handle(_refreshTokenCommand, _cancellationToken);
        
        var JwtResult = new JwtToken();
        result.Map(x => JwtResult = x);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        JwtResult.Should().BeEquivalentTo(expectedFullToken);

    }
    
    /*
    var created = DateTime.Now;
        var expires = DateTime.Now.AddDays(1);
        var fullToken = new JwtToken()
        {
            Created = created,
            RefreshToken = "refreshToken",
            Expires = expires,
            Token = "token"
        };

        var expectedFullToken = new JwtToken()
        {
            Created = created,
            RefreshToken = "refreshToken",
            Expires = expires,
            Token = "token"
        };


handler_ShouldReturnNewToken_WhenRefreshTokenIsValid()*/
}