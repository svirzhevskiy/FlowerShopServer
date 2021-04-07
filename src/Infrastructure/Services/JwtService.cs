using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Exceptions;
using Application.Services;
using Application.Settings;
using Microsoft.IdentityModel.Tokens;
using Models.Auth;

namespace Services
{
    public class JwtService : IJwtService
    {
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokens =>
            _usersRefreshTokens.ToImmutableDictionary();

        private readonly ConcurrentDictionary<string, RefreshToken>
            _usersRefreshTokens; // can store in a database or a distributed cache

        private readonly JwtConfig _jwtConfig;
        private readonly byte[] _secret;

        public JwtService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
            _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        }

        // optional: clean up expired refresh tokens
        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            foreach (var expiredToken in _usersRefreshTokens.Where(x => x.Value.ExpireAt < now))
            {
                _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
            }
        }

        // can be more specific to ip, user agent, device name, etc.
        public void RemoveRefreshTokenByUserName(string email)
        {
            foreach (var refreshToken in _usersRefreshTokens.Where(x => x.Value.UserName == email))
            {
                _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }

        public JwtAuthResult GenerateTokens(string email, Claim[] claims, DateTime now)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtConfig.Issuer,
                string.Empty,
                claims,
                expires: now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret),
                    SecurityAlgorithms.HmacSha256Signature));


            var accessToken = string.Empty;
            try
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var refreshToken = new RefreshToken
            {
                UserName = email,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(_jwtConfig.RefreshTokenExpiration)
            };
            _usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (_, _) => refreshToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public JwtAuthResult Refresh(string refreshToken, string accessToken)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw AppError.InvalidToken;
            }

            var userName = principal.Identity?.Name;
            if (!_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
            {
                throw AppError.InvalidToken;
            }

            var now = DateTime.Now;
            if (existingRefreshToken.UserName != userName || existingRefreshToken.ExpireAt < now)
            {
                throw AppError.InvalidToken;
            }

            return GenerateTokens(userName, principal.Claims.ToArray(), now); // need to recover the original claims
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw AppError.InvalidToken;
            }

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtConfig.Issuer,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}