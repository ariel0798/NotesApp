using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace NotesApp.ApplicationCore.Tests.Unit.Authentication.Commands;

public class LoginCommandHandlerTests
{
    private readonly LoginCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly IAuthService _authService = Substitute.For<IAuthService>();
    private readonly LoginCommand _loginCommand = new LoginCommand("test@test.com", "test123");
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    public LoginCommandHandlerTests()
    {
        _sut = new LoginCommandHandler(_unitOfWork,_passwordHasher,_authService);
    }

    [Fact]
    public async Task Handle_ShouldReturnInvalidCredentialException_WhenUserDoesNotExist()
    {
        // Arrange
        _unitOfWork.Users.GetUserByEmail(Arg.Any<string>()).ReturnsNull();
        
        var expectedException = new InvalidCredentialException("Invalid Credentials");
        
        //  Act
        var result = await  _sut.Handle(_loginCommand, _cancellationToken);
        
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
    public async Task Handle_ShouldReturnInvalidCredentialException_WhenPasswordIsIncorrect()
    {
        // Arrange
        
        var user = new User()
        {
            UserId = 1,
            Name = "Richard",
            LastName = "Cruz",
            Email = "test@test.com",
            PasswordHash = new byte[] { 0x20 },
            PasswordSalt = new byte[] { 0x20, 0x20 }
        };
        
        _unitOfWork.Users.GetUserByEmail(_loginCommand.Email).Returns(user);
        _passwordHasher.VerifyPasswordHash(_loginCommand.Password, user.PasswordHash, user.PasswordSalt).Returns(false);
        
       var expectedException = new InvalidCredentialException("Invalid Credentials");
        
        //  Act
        var result = await  _sut.Handle(_loginCommand, _cancellationToken);
        
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
    public async Task Handle_ShouldReturnJwt_WhenCredentialsAreRight()
    {
        // Arrange
        var user = new User()
        {
            UserId = 1,
            Name = "Richard",
            LastName = "Cruz",
            Email = "test@test.com",
            PasswordHash = new byte[] { 0x20 },
            PasswordSalt = new byte[] { 0x20, 0x20 }
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
        
        _unitOfWork.Users.GetUserByEmail(_loginCommand.Email).Returns(user);
        _passwordHasher.VerifyPasswordHash(_loginCommand.Password, user.PasswordHash, user.PasswordSalt).Returns(true);
        _authService.SetTokenAndRefreshToken(user).Returns(fullToken);
        
        //  Act
        var result = await  _sut.Handle(_loginCommand, _cancellationToken);
        var JwtResult = new JwtToken();
        result.Map(x => JwtResult = x);
        
        // Assert
        JwtResult.Should().BeEquivalentTo(expectedFullToken);
        result.IsSuccess.Should().BeTrue();
    }

}