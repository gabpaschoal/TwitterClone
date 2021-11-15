using EasyValidation.Core;
using EasyValidation.Core.Extensions;
using TwitterClone.Application.Commands.User;
using TwitterClone.Resouces;

namespace TwitterClone.Application.Validators.User;

public class UserCreateValidator : Validation<UserCreateCommand>
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
    }
}
