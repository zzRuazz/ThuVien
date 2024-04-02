using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ProductImageRepository : GenericRepository, IProductImageRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public ProductImageRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public Task<int> CreateAsync(ProductImage entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<ProductImage>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<ProductImage?> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(ProductImage entity)
    {
        throw new NotImplementedException();
    }
}
