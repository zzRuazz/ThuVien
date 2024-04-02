using Models.Entities;

namespace Models.Cache;

public class AccountCache
{
    public Account AccountData { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}
