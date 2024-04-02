namespace Models.Request;

/// <summary>
/// LoginRequest
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// IpAddress
    /// </summary>
    public string IpAddress { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;
}
