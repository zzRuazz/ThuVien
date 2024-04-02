namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IUnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// IAccountRepository
    /// </summary>
    IAccountRepository Account { get; }

    /// <summary>
    /// ICategoryRepository
    /// </summary>
    ICategoryRepository Category { get; }

    /// <summary>
    /// IManufactureRepository
    /// </summary>
    IManufactureRepository Manufacture { get; }

    /// <summary>
    /// IProductRepository
    /// </summary>
    IProductRepository Product { get; }

    /// <summary>
    /// IProductPropertyRepository
    /// </summary>
    IProductPropertyRepository ProductProperty { get; }

    /// <summary>
    /// IProductDetailRepository
    /// </summary>
    IProductDetailRepository ProductDetail { get; }

    /// <summary>
    /// IProductImageRepository
    /// </summary>
    IProductImageRepository ProductImage { get; }

    /// <summary>
    /// Begin
    /// </summary>
    void Begin();

    /// <summary>
    /// Commit
    /// </summary>
    void Commit();
}
