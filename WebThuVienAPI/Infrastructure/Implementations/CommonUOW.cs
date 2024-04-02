using Microsoft.Data.SqlClient;
using Common.Abstractions;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

public class CommonUOW
{
    /// <summary>
    /// ConnectionString
    /// </summary>
    protected static readonly string ConnectionString = "";

    /// <summary>
    /// IDbConnection
    /// </summary>
    protected IDbConnection _connection;

    /// <summary>
    /// IDbTransaction
    /// </summary>
    protected IDbTransaction _transaction;

    /// <summary>
    /// ILogProvider
    /// </summary>
    protected readonly ILogProvider _logProvider;

    /// <summary>
    /// disposed flag
    /// </summary>
    protected bool _disposed;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configProvider"></param>
    protected CommonUOW(IConfigProvider configProvider)
    {
        var connectionString = configProvider.GetConfigString(ConnectionString);
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        //Begin();
    }

    /// <inheritdoc/>
    public void Begin()
    {
        _transaction = _connection.BeginTransaction();
    }

    /// <inheritdoc/>
    public void Commit()
    {
        if (_transaction == null) return;

        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction.Dispose();
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        _disposed = true;
    }

    /// <summary>
    /// ~CommonUOW
    /// </summary>
    ~CommonUOW()
    {
        Dispose(false);
    }
}
