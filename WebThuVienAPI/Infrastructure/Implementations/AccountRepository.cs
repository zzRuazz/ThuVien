using Dapper;
using DapperExtensions;
using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class AccountRepository : GenericRepository, IAccountRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public AccountRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public async Task<int> CreateAsync(Account entity)
    {
        try
        {
            var sql = "INSERT INTO Account VALUES (@Id, @Email, @Fullname, @Password, @Address, @Avatar, @IsActived, @IsDeleted);";
            var result = await Connection.ExecuteAsync(sql, entity);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return 0;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var sql = "SELECT * FROM Account WHERE Id = @Id";
            var find = await Connection.QueryFirstOrDefaultAsync<Account>(sql, param: new { Id = id }, transaction: Transaction);
            
            if (find != null)
            {
                var result = await Connection.DeleteAsync(find, transaction: Transaction);
                return result;
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Account>?> GetAllAsync()
    {
        try
        {
            var sql = "SELECT * FROM Account";
            var result = await Connection.QueryAsync<Account>(sql, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<Account?> GetAsync(string id)
    {
        try
        {
            var sql = "SELECT * FROM Account WHERE Id = @Id";
            var result = await Connection.QueryFirstOrDefaultAsync<Account>(sql, param: new { Id = id }, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<Account?> GetOneByEmail(string email)
    {
        try
        {
            var sql = "SELECT * FROM Account WHERE Email = @Email";
            var result = await Connection.QueryFirstOrDefaultAsync<Account>(sql, param: new { Email = email }, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(Account entity)
    {
        // TODO: Implement update
        throw new NotImplementedException();
    }
}
