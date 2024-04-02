using Models.Entities;
using Models.Jwt;
using Models.Jwt.Enums;

namespace Common.Abstractions;

/// <summary>
/// IJwtProvider
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// GenerateJwt
    /// </summary>
    /// <param name="account"></param>
    /// <param name="role"></param>
    /// <param name="sub"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public string? GenerateJwt(Account account, string role, string audiencer, string issuer, JwtType type);

    /// <summary>
    /// ValidateJwt
    /// </summary>
    /// <param name="jwt"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool ValidateJwt(string jwt, JwtType type);

    /// <summary>
    /// DecodeJwt
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public (JwtHeader? header, JwtPayload? payload) DecodeJwt(string token, JwtType type);

    /// <summary>
    /// DecodeJwtToPayload
    /// </summary>
    /// <param name="token"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public JwtPayload? DecodeJwtToPayload(string token, JwtType type);
}
