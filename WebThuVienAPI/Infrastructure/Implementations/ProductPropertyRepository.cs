using Dapper;
using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using System.Data;
using System.Text;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ProductPropertyRepository : GenericRepository, IProductPropertyRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public ProductPropertyRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public async Task<int> CreateAsync(ProductProperty entity)
    {
        try
        {
            var sql = @"INSERT INTO ProductProperty
                            (Id, Name, Slug, CategoryName, CategoryId, IsActived, CreatedAt, IsDeleted)
                        VALUES
                            (@Id, @Name, @Slug, @CategoryName, @CategoryId, @IsActived, @CreatedAt, @IsDeleted);";

            var result = await Connection.ExecuteAsync(sql, entity);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return 0;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var find = await GetAsync(id);

            if (find != null)
            {
                var sql = @"DELETE FROM [ProductProperty] WHERE [Id] = @Id";
                var result = await Connection.ExecuteAsync(sql, find);

                if (result > 0)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<DataPaging<ProductProperty>?> FilterDataPaging(ProductPropertyFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var condition = new List<string>();

            if (filter.CategoryId != null)
            {
                condition.Add("CategoryId = @CategoryId");
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                condition.Add("Name like '%@Name%'");
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                condition.Add("Slug like '%@Slug%'");
            }

            if (filter.IsActived != null)
            {
                condition.Add("IsActived = @IsActived");
            }

            var sqlCount = "SELECT COUNT(Id) FROM ProductProperty WHERE Id <> '' ";
            var sql = "SELECT * FROM ProductProperty WHERE Id <> '' ";

            if (condition.Any())
            {
                sqlCount += " AND " + $"{string.Join("AND", condition)}";
                sql += " AND " + $"{string.Join("AND", condition)}";
            }

            sql += " ORDER BY CreatedAt DESC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            var count = await CountBySql(sqlCount, filter);
            var data = await GetBySql(sql, filter);

            return new DataPaging<ProductProperty> { Data = data, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProductProperty>?> GetAllAsync()
    {
        try
        {
            var sql = "SELECT * FROM ProductProperty ORDER BY CreatedAt DESC";
            var result = await Connection.QueryAsync<ProductProperty>(sql);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<ProductProperty?> GetAsync(string id)
    {
        try
        {
            var sql = "SELECT * FROM ProductProperty WHERE Id = @Id";
            var result = await Connection.QueryFirstOrDefaultAsync<ProductProperty>(sql, param: new { Id = id }, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(ProductProperty entity)
    {
        try
        {
            var find = await GetAsync(entity.Id);

            if (find != null)
            {
                var sbSQL = new StringBuilder();
                sbSQL.Append("UPDATE [ProductProperty] SET ");

                if (entity.Name != find.Name)
                {
                    sbSQL.Append(" Name = @Name, ");
                }

                if (entity.Slug != find.Slug)
                {
                    sbSQL.Append(" Slug = @Slug, ");
                }

                if (entity.IsActived != find.IsActived)
                {
                    sbSQL.Append(" IsActived = @IsActived, ");
                }

                if (entity.IsDeleted != find.IsDeleted)
                {
                    sbSQL.Append(" IsDeleted = @IsDeleted, ");
                }

                if (entity.CategoryId != find.CategoryId)
                {
                    sbSQL.Append(" CategoryId = @CategoryId, ");
                }

                if (entity.CategoryName != find.CategoryName)
                {
                    sbSQL.Append(" CategoryName = @CategoryName, ");
                }

                sbSQL.Append(" UpdatedAt = @UpdatedAt ");
                sbSQL.Append(" WHERE [Id] = @Id ");
                var sql = sbSQL.ToString();
                var result = await Connection.ExecuteAsync(sql, entity);

                if (result > 0)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    private async Task<IEnumerable<ProductProperty>?> GetBySql(string sql, object filter)
    {
        try
        {
            var result = await Connection.QueryAsync<ProductProperty>(sql, filter, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return null;
        }
    }
}
