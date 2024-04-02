using Models.Entities;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IAccountRepository
/// </summary>
public interface IAccountRepository : IGenericRepository<Account>
{
    /// <summary>
    /// GetOneByEmail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Account?> GetOneByEmail(string email);
}
