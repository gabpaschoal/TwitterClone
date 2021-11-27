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

    [Fact(DisplayName = "Should add error when Name has less or more than the limit")]
    public void Should_add_error_when_Name_has_less_or_more_than_the_limit()
    {
        var sutMinLenghtReq = MakeSut(name: "".PadLeft(4, 'X'));
        sutMinLenghtReq.ResultData.IsValid.Should().BeFalse();
        sutMinLenghtReq.ResultData.FieldErrors.Single().Key.Should().Be("Name");

        var sutMaxLenghtReq = MakeSut(name: "".PadLeft(101, 'X'));
        sutMaxLenghtReq.ResultData.IsValid.Should().BeFalse();
        sutMaxLenghtReq.ResultData.FieldErrors.Single().Key.Should().Be("Name");
    }

    [Fact(DisplayName = "Should add no error when Name is in the limit")]
    public void Should_add_no_error_when_Name_is_in_the_limit()
    {
        var sutMinLenghtReq = MakeSut(name: "".PadLeft(5, 'X'));
        sutMinLenghtReq.ResultData.IsValid.Should().BeTrue();
        sutMinLenghtReq.ResultData.FieldErrors.Should().BeEmpty();

        var sutMaxLenghtReq = MakeSut(name: "".PadLeft(100, 'X'));
        sutMaxLenghtReq.ResultData.IsValid.Should().BeTrue();
        sutMaxLenghtReq.ResultData.FieldErrors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Should add error when NickName is null or empty")]
    public void Should_add_error_when_NickName_is_null_or_empty()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var sutNull = MakeSut(nickName: null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        sutNull.ResultData.IsValid.Should().BeFalse();
        sutNull.ResultData.FieldErrors.Single().Key.Should().Be("NickName");

        var sutEmpty = MakeSut(nickName: "");
        sutEmpty.ResultData.IsValid.Should().BeFalse();
        sutEmpty.ResultData.FieldErrors.Single().Key.Should().Be("NickName");
    }

    [Fact(DisplayName = "Should add error when NickName has less or more than the limit")]
    public void Should_add_error_when_NickName_has_less_or_more_than_the_limit()
    {
        var sutMinLenghtReq = MakeSut(nickName: "".PadLeft(4, 'X'));
        sutMinLenghtReq.ResultData.IsValid.Should().BeFalse();
        sutMinLenghtReq.ResultData.FieldErrors.Single().Key.Should().Be("NickName");

        var sutMaxLenghtReq = MakeSut(nickName: "".PadLeft(31, 'X'));
        sutMaxLenghtReq.ResultData.IsValid.Should().BeFalse();
        sutMaxLenghtReq.ResultData.FieldErrors.Single().Key.Should().Be("NickName");
    }

    [Fact(DisplayName = "Should add no error when NickName is in the limit")]
    public void Should_add_no_error_when_NickName_is_in_the_limit()
    {
        var sutMinLenghtReq = MakeSut(nickName: "".PadLeft(5, 'X'));
        sutMinLenghtReq.ResultData.IsValid.Should().BeTrue();
        sutMinLenghtReq.ResultData.FieldErrors.Should().BeEmpty();

        var sutMaxLenghtReq = MakeSut(nickName: "".PadLeft(30, 'X'));
        sutMaxLenghtReq.ResultData.IsValid.Should().BeTrue();
        sutMaxLenghtReq.ResultData.FieldErrors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Should add error when Email is null or empty")]
    public void Should_add_error_when_Email_is_null_or_empty()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var sutNull = MakeSut(email: null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        sutNull.ResultData.IsValid.Should().BeFalse();
        sutNull.ResultData.FieldErrors.Single().Key.Should().Be("Email");

        var sutEmpty = MakeSut(email: "");
        sutEmpty.ResultData.IsValid.Should().BeFalse();
        sutEmpty.ResultData.FieldErrors.Single().Key.Should().Be("Email");
    }

    [Fact(DisplayName = "Should add error when Email has less or more than the limit")]
    public void Should_add_error_when_Email_has_less_or_more_than_the_limit()
    {
        var sutMinLenghtReq = MakeSut(email: "".PadLeft(4, 'X'));
        sutMinLenghtReq.ResultData.IsValid.Should().BeFalse();
        sutMinLenghtReq.ResultData.FieldErrors.Single().Key.Should().Be("Email");

        var sutMaxLenghtReq = MakeSut(email: "".PadLeft(71, 'X'));
        sutMaxLenghtReq.ResultData.IsValid.Should().BeFalse();
        sutMaxLenghtReq.ResultData.FieldErrors.Single().Key.Should().Be("Email");
    }

    [Fact(DisplayName = "Should add no error when Email is in the limit")]
    public void Should_add_no_error_when_Email_is_in_the_limit()
    {
        var sutMinLenghtReq = MakeSut(email: "".PadLeft(5, 'X'));
        sutMinLenghtReq.ResultData.IsValid.Should().BeTrue();
        sutMinLenghtReq.ResultData.FieldErrors.Should().BeEmpty();

        var sutMaxLenghtReq = MakeSut(email: "".PadLeft(70, 'X'));
        sutMaxLenghtReq.ResultData.IsValid.Should().BeTrue();
        sutMaxLenghtReq.ResultData.FieldErrors.Should().BeEmpty();
    }
}
