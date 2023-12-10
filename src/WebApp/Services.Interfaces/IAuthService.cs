using Shared.DataTransferObjects;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task SignUpAsync(SignUpDto userForSignUpDto);
        Task<bool> ValidateUserAsync(SignInDto userForSignInDto);
        Task<TokenDto> CreateTokenAsync(bool populateExp);
        Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    }
}
