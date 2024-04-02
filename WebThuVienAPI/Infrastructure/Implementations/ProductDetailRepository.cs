using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ProductDetailRepository : GenericRepository, IProductDetailRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public ProductDetailRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public Task<int> CreateAsync(ProductDetail entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ProductDetail>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<ProductDetail?> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(ProductDetail entity)
    {
        throw new NotImplementedException();
    }
}
