using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using System.Data;
using Models.Common;
using Models.Filter;

namespace WebThuVienAPI.Infrastructure.Implementations;

internal class ProductRepository : GenericRepository, IProductRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public ProductRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public Task<int> CreateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<DataPaging<Product>?> FilterDataPaging(ProductFilter filter)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Product>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<Product?> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }
}
