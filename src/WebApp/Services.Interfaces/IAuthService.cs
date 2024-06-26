﻿using Shared.DataTransferObjects;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task SignUpAsync(SignUpDto userForSignUpDto);
        Task<bool> ValidateUserAsync(SignInDto userForSignInDto);
        Task<(TokensInfo, AuthInfo)> CreateTokenAsync();
        Task<(TokensInfo, AuthInfo)> RefreshTokenAsync(TokensInfo tokensInfo);
    }
}
