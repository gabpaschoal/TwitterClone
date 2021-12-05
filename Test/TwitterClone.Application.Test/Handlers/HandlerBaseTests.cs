using EasyValidation.DependencyInjection;
using FluentAssertions;
using Moq;
using System.Linq;
using TwitterClone.Application.Handlers;
using Xunit;

namespace TwitterClone.Application.Test.Handlers;

public partial class HandlerBaseTests
{
    private static HandlerBaseStub MakeSut(IHandlerBus? handlerBus = null)
    {
        handlerBus ??= new Mock<IHandlerBus>().Object;

        return new(handlerBus);
    }

    [Fact(DisplayName = "HandlerBase should be valid when is created")]
    public void HandlerBase_should_be_valid_when_is_created_and_invalid_after_add_an_error()
    {
        var sut = MakeSut();

        sut.IsValid.Should().BeTrue();
        sut.ValidResponseAsync().IsValid.Should().BeTrue();
        sut.InvalidResponseAsync().IsValid.Should().BeTrue();
        sut.ValidResponse().Result.IsValid.Should().BeTrue();
        sut.InvalidResponse().Result.IsValid.Should().BeTrue();
        sut.IsInvalid.Should().BeFalse();

        string key = "_key";
        sut.AddError(key, "this key is invalid");

        sut.IsValid.Should().BeFalse();
        sut.ValidResponseAsync().IsValid.Should().BeTrue();
        sut.IsInvalid.Should().BeTrue();
        sut.InvalidResponseAsync().IsValid.Should().BeFalse();
        sut.ValidResponse().Result.IsValid.Should().BeTrue();
        sut.InvalidResponse().Result.IsValid.Should().BeFalse();
        sut.HandleExecutionCalls.Should().Be(0);
    }

    [Fact(DisplayName = "Handle should not execute HandleExecution when command is Invalid")]
    public void Handle_should_not_execute_HandleExecution_when_command_is_Invalid()
    {
        CustomResultData invalidDataResult = new();
        invalidDataResult.IsValid.Should().BeTrue();
        invalidDataResult.AddFieldError("_key", "this is a message");
        invalidDataResult.IsValid.Should().BeFalse();

        var validatorLocatorMock = new Mock<IValidatorLocator>();
        validatorLocatorMock.Setup(x => x.ValidateCommand(It.IsAny<StubCommand>())).Returns(invalidDataResult);

        var handlerBusMock = new Mock<IHandlerBus>();
        handlerBusMock.Setup(x => x.Validator).Returns(validatorLocatorMock.Object);

        var sut = MakeSut(handlerBusMock.Object);

        CustomResultData commandResult = sut.Handle(It.IsAny<StubCommand>(), System.Threading.CancellationToken.None).Result;

        commandResult.IsValid.Should().BeFalse();
        sut.HandleExecutionCalls.Should().Be(0);
    }

    [Fact(DisplayName = "Handle should execute HandleExecution when command is Valid")]
    public void Handle_should_execute_HandleExecution_when_command_is_Valid()
    {
        CustomResultData invalidDataResult = new();
        invalidDataResult.IsValid.Should().BeTrue();

        var validatorLocatorMock = new Mock<IValidatorLocator>();
        validatorLocatorMock.Setup(x => x.ValidateCommand(It.IsAny<StubCommand>())).Returns(invalidDataResult);

        var handlerBusMock = new Mock<IHandlerBus>();
        handlerBusMock.Setup(x => x.Validator).Returns(validatorLocatorMock.Object);

        var sut = MakeSut(handlerBusMock.Object);

        CustomResultData commandResult = sut.Handle(It.IsAny<StubCommand>(), System.Threading.CancellationToken.None).Result;

        commandResult.IsValid.Should().BeTrue();
        sut.HandleExecutionCalls.Should().Be(1);
    }

    [Fact(DisplayName = "AddError should add errors to the intern Response")]
    public void AddError_should_add_errors_to_the_intern_Response()
    {
        var sut = MakeSut();

        sut.IsValid.Should().BeTrue();
        sut.IsInvalid.Should().BeFalse();

        string key = "_key";
        sut.AddError(key, "this key is invalid");

        sut.IsValid.Should().BeFalse();
        sut.IsInvalid.Should().BeTrue();

        var invalidCommandResult = sut.InvalidResponseAsync();
        invalidCommandResult.IsValid.Should().BeFalse();

        invalidCommandResult.FieldErrors.Single().Key.Should().Be(key);
    }
}
