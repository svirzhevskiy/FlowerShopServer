using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models.Auth;

namespace Application.Services
{
    public interface IJwtService
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokens { get; }
        JwtAuthResult GenerateTokens(string email, Claim[] claims, DateTime now);
        JwtAuthResult Refresh(string refreshToken, string accessToken);
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByUser(string email);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}