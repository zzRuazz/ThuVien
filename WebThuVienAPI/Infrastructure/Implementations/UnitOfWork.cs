using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// ConnectionString
    /// </summary>
    private static readonly string ConnectionString = "ConnectionString";

    /// <summary>
    /// IDbConnection
    /// </summary>
    private IDbConnection _connection;

    /// <summary>
    /// IDbTransaction
    /// </summary>
    private IDbTransaction _transaction;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    /// <summary>
    /// disposed flag
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Instance of IAccountRepository
    /// </summary>
    private IAccountRepository _account;

    /// <summary>
    /// Instance of ICategoryRepository
    /// </summary>
    private ICategoryRepository _category;

    /// <summary>
    /// Instance of IManufactureRepository
    /// </summary>
    private IManufactureRepository _manufacture;

    /// <summary>
    /// Instance of IProductRepository
    /// </summary>
    private IProductRepository _product;

    /// <summary>
    /// Instance of IProductPropertyRepository
    /// </summary>
    private IProductPropertyRepository _productProperty;

    /// <summary>
    /// Instance of IProductDetailRepository
    /// </summary>
    private IProductDetailRepository _productDetail;

    /// <summary>
    /// Instance of IProductImageRepository
    /// </summary>
    private IProductImageRepository _productImage;

    /// <inheritdoc/>
    public IAccountRepository Account
    {
        get { return _account ??= new AccountRepository(_transaction, _connection, _logProvider); }
    }

    /// <inheritdoc/>
    public ICategoryRepository Category
    {
        get { return _category ??= new CategoryRepository(_transaction, _connection, _logProvider); }
    }

    /// <inheritdoc/>
    public IManufactureRepository Manufacture
    {
        get { return _manufacture ??= new ManufactureRepository(_transaction, _connection, _logProvider); }
    }

    /// <inheritdoc/>
    public IProductRepository Product
    {
        get { return _product ??= new ProductRepository(_transaction, _connection, _logProvider); }
    }

    /// <inheritdoc/>
    public IProductPropertyRepository ProductProperty
    {
        get { return _productProperty ??= new ProductPropertyRepository(_transaction, _connection, _logProvider); }
    }

    /// <inheritdoc/>
    public IProductDetailRepository ProductDetail
    {
        get { return _productDetail ??= new ProductDetailRepository(_transaction, _connection, _logProvider); }
    }

    /// <inheritdoc/>
    public IProductImageRepository ProductImage
    {
        get { return _productImage ??= new ProductImageRepository(_transaction, _connection, _logProvider); }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configProvider"></param>
    public UnitOfWork(IConfigProvider configProvider)
    {
        var connectionString = configProvider.GetConfigString(ConnectionString);
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        //Begin();
    }

    /// <summary>
    /// Reset Repository
    /// </summary>
    private void ResetRepository()
    {
        _account = null;
        _category = null;
        _manufacture = null;
        _product = null;
        _productProperty = null;
        _productDetail = null;
        _productImage = null;
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
            ResetRepository();
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
    private void Dispose(bool disposing)
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
    /// ~UnitOfWork
    /// </summary>
    ~UnitOfWork()
    {
        Dispose(false);
    }
}
