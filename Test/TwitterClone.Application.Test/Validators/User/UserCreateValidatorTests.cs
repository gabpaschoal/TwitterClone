using FluentAssertions;
using System.Linq;
using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Validators.User;
using Xunit;

namespace TwitterClone.Application.Test.Validators.User;

public class UserCreateValidatorTests
{
    private static UserCreateValidator MakeSut(
        string name = "Gustav Jeremy",
        string nickName = "gustav123",
        string email = "gustav123@mail.com",
        string password = "validPassword",
        string passwordConfirmation = "validPassword")
    {

        UserCreateCommand user = new(name, nickName, email, password, passwordConfirmation);

        UserCreateValidator validator = new();

        validator.SetValue(user);
        validator.Validate();

        return validator;
    }

    [Fact(DisplayName = "Should add error when Name is null or empty")]
    public void Should_add_error_when_Name_is_null_or_empty()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var sutNull = MakeSut(name: null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        sutNull.ResultData.IsValid.Should().BeFalse();
        sutNull.ResultData.FieldErrors.Single().Key.Should().Be("Name");

        var sutEmpty = MakeSut(name: "");
        sutEmpty.ResultData.IsValid.Should().BeFalse();
        sutEmpty.ResultData.FieldErrors.Single().Key.Should().Be("Name");
    }
}
