using System;
namespace Project.Domain.Shared
{
    public enum TokenTypes
    {
        RefreshToken,
        ChangePasswordToken,
        EmailConfirmationToken,
        PhoneConfirmationToken,
        AuthenticationToken,
        AuthorizationToken
    }
}
