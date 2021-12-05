using EasyValidation.Core;
using EasyValidation.Core.Extensions;
using TwitterClone.Application.Commands.User;

namespace TwitterClone.Application.Validators.User;

public class UserSignInValidator : Validation<UserSignInCommand>
{
    public override void Validate()
    {
        ForMember(x => x.Email).IsRequired();
        ForMember(x => x.Password).IsRequired();
    }
}
