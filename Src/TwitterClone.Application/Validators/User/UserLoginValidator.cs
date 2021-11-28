using EasyValidation.Core;
using EasyValidation.Core.Extensions;
using TwitterClone.Application.Commands.User;

namespace TwitterClone.Application.Validators.User;

public class UserLoginValidator : Validation<UserLoginCommand>
{
    public override void Validate()
    {
        ForMember(x => x.Email).IsRequired();
        ForMember(x => x.Password).IsRequired();
    }
}
