using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using WebThuVienAPI.Models.Context;
using Common.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration) : base(context, logProvider, configuration)
    {
    }

    /// <inheritdoc/>
    public async Task<Account?> GetOneByEmail(string email)
    {
        try
        {
            var entity = await _context.Set<Account>().FirstOrDefaultAsync(x => x.Email.Equals(email));
            return entity;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }
}
