using EasyValidation.Core;
using EasyValidation.Core.Extensions;
using TwitterClone.Application.Commands.User;
using TwitterClone.Resources;

namespace TwitterClone.Application.Validators.User;

public class UserSignUpValidator : Validation<UserSignUpCommand>
{
    public override void Validate()
    {
        ForMember(x => x.Name)
            .IsRequired()
            .ShouldBeBetweenLenght(5, 100, ValidationMessage.HasMinLenghtMessage, ValidationMessage.HasMaxLenghtMessage);

        ForMember(x => x.NickName)
            .IsRequired()
            .ShouldBeBetweenLenght(5, 30, ValidationMessage.HasMinLenghtMessage, ValidationMessage.HasMaxLenghtMessage);

        ForMember(x => x.Email)
            .IsRequired()
            .ShouldBeBetweenLenght(5, 70, ValidationMessage.HasMinLenghtMessage, ValidationMessage.HasMaxLenghtMessage);

        ForMember(x => x.Password)
            .IsRequired()
            .ShouldBeBetweenLenght(5, 70, ValidationMessage.HasMinLenghtMessage, ValidationMessage.HasMaxLenghtMessage);

        ForMember(x => x.PasswordConfirmation)
            .IsRequired()
            .ShouldBeBetweenLenght(5, 70, ValidationMessage.HasMinLenghtMessage, ValidationMessage.HasMaxLenghtMessage);

        var password = GetCommandProperty(x => x.Password);
        var passwordConfirmation = GetCommandProperty(x => x.PasswordConfirmation);

        if (password is not null && passwordConfirmation is not null && password != passwordConfirmation)
            AddError("PasswordConfirmation", ValidationMessage.PasswordConfirmationDoesntMatchWithPassword);
    }
}
