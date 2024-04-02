using Common.Abstractions;
using Dapper;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <summary>
/// GenericRepository
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository
{
    /// <summary>
    /// IDbTransaction
    /// </summary>
    protected IDbTransaction Transaction { get; }

    /// <summary>
    /// IDbConnection
    /// </summary>
    protected IDbConnection Connection { get; }

    /// <summary>
    /// ILogProvider
    /// </summary>
    protected readonly ILogProvider _logProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    /// <param name="objectProvider"></param>
    public GenericRepository(
        IDbTransaction transaction,
        IDbConnection connection,
        ILogProvider logProvider)
    {
        Transaction = transaction;
        Connection = connection;
        _logProvider = logProvider;
    }

    protected async Task<long> CountBySql(string sql, object filter)
    {
        try
        {
            var count = await Connection.QueryAsync<int>(sql, filter, transaction: Transaction);

            if (!count.Any())
            {
                return 0;
            }

            return (long)count.First();
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return 0;
        }
    }
}
