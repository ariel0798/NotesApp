using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using HashidsNet;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Contracts.User.Responses;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Errors.Messages;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace NotesApp.ApplicationCore.Tests.Unit.Authentication.Commands;

public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _sut;
    private readonly IValidator<RegisterCommand> _validator = Substitute.For<IValidator<RegisterCommand>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly IHashids _hashids = Substitute.For<IHashids>();
    private readonly RegisterCommand _registerCommand;
    private readonly CancellationToken _cancellationToken = new();
    private readonly User _user;


    public RegisterCommandHandlerTests()
    {
        _sut = new RegisterCommandHandler(_validator, _unitOfWork, _mapper, _passwordHasher, _hashids);
        _registerCommand = new RegisterCommand(
            "Richard",
            "Cruz",
            "test@test.com",
            "test123");
        
        _user = new User()
        {
            UserId = 1,
            Name = "Richard",
            LastName = "Cruz",
            Email = "test@test.com",
            PasswordHash = new byte[] { 0x20 },
            PasswordSalt = new byte[] { 0x20, 0x20 }
        };
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationException_WhenRequestNotValid()
    {
        // Arrange
        var validation = Substitute.For<ValidationResult>();
        validation.IsValid.Returns(false);
        validation.Errors = new List<ValidationFailure>() { new ValidationFailure("Name", "can not be null") };
        
        _validator.ValidateAsync(_registerCommand,_cancellationToken).Returns(validation);
        
        var expectedException = new ValidationException(validation.Errors);
        
        //  Act
        var result = await _sut.Handle(_registerCommand, _cancellationToken);
        
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
    public async Task Handle_ShouldReturnEmailDuplicatedException_WhenUserWithEmailExists()
    {
        // Arrange
        var validation = Substitute.For<ValidationResult>();
        validation.IsValid.Returns(true);
        
        _validator.ValidateAsync(_registerCommand,_cancellationToken).Returns(validation);
        
        _unitOfWork.Users.GetUserByEmail(_registerCommand.Email).Returns(_user);
        
        var expectedException = new EmailDuplicatedException( "Email already exists");

        //  Act
        var result = await _sut.Handle(_registerCommand, _cancellationToken);
        
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
    public async Task Handler_ShouldCreateUser_WhenUserIsValid()
    {
        // Arrange
        var validation = Substitute.For<ValidationResult>();
        validation.IsValid.Returns(true);
        
        _validator.ValidateAsync(_registerCommand,_cancellationToken).Returns(validation);
        
        _unitOfWork.Users.GetUserByEmail(_registerCommand.Email).ReturnsNull();

        _mapper.Map<User>(_registerCommand).Returns(_user);

        var userResponse = new UserResponse()
        {
            UserId = "1",
            Name = "Richard",
            LastName = "Cruz",
            Email = "test@test.com",
        };
        
        _mapper.Map<UserResponse>(_user).Returns(userResponse);

        _hashids.Encode(Arg.Any<int>()).Returns("Rc123456789");
        
        var expectedUserResponse = new UserResponse()
        {
            UserId = "Rc123456789",
            Name = "Richard",
            LastName = "Cruz",
            Email = "test@test.com",
        };
        
        //  Act
        var result = await _sut.Handle(_registerCommand, _cancellationToken);
        
        // Assert
        var userResponseResult = new UserResponse();
        result.Map(x => userResponseResult = x);

        userResponse.Should().BeEquivalentTo(expectedUserResponse);
        
        _passwordHasher.Received(1).CreatePasswordHash(_registerCommand.Password,out Arg.Any<byte[]>(),out Arg.Any<byte[]>());
        await _unitOfWork.Users.Received(1).Add(Arg.Any<User>());
        await _unitOfWork.Notes.Received(1).Add(Arg.Any<Note>());
        await _unitOfWork.Received(1).Save();

    }
}