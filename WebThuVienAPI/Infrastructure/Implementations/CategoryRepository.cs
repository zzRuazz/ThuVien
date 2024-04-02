using Dapper;
using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using System.Data;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class CategoryRepository : GenericRepository, ICategoryRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public CategoryRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public async Task<int> CreateAsync(Category entity)
    {
        try
        {
            var sql = @"INSERT INTO Category
                            (Id, Name, Slug, Image, ParentId, IsActived, CreatedAt, IsDeleted)
                        VALUES
                            (@Id, @Name, @Slug, @Image, @ParentId, @IsActived, @CreatedAt, @IsDeleted);";

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
                var sql = @"DELETE FROM [Category] WHERE [Id] = @Id";
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
    public async Task<bool> UpdateAsync(Category entity)
    {
        try
        {
            var find = await GetAsync(entity.Id);

            if (find != null)
            {
                var sql = @"UPDATE [Category]
                        SET
                            [Name] = @Name,
                            [Slug] = @Slug,
                            [Image] = @Image,
                            [ParentId] = @ParentId,
                            [IsActived] = @IsActived,
                            [UpdatedAt] = @UpdatedAt,
                            [UpdatedBy] = @UpdatedBy,
                            [DeletedAt] = @DeletedAt,
                            [DeletedBy] = @DeletedBy,
                            [IsDeleted] = @IsDeleted
                        WHERE
                            [Id] = @Id";

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

    /// <inheritdoc/>
    public async Task<IEnumerable<Category>?> GetAllAsync()
    {
        try
        {
            var sql = "SELECT * FROM Category ORDER BY CreatedAt DESC";
            var result = await Connection.QueryAsync<Category>(sql);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<Category?> GetAsync(string id)
    {
        try
        {
            var sql = "SELECT * FROM Category WHERE Id = @Id";
            var result = await Connection.QueryFirstOrDefaultAsync<Category>(sql, param: new { Id = id }, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Category>?> GetActiveParentCategories()
    {
        try
        {
            var sql = "SELECT * FROM Category WHERE IsActived = 1 AND ParentId IS NULL OR ParentId = '' ORDER BY CreatedAt DESC";
            var result = await Connection.QueryAsync<Category>(sql);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Category>?> GetActiveChildrenCategoriesByParentId(string parentId)
    {
        try
        {
            var sql = "SELECT * FROM Category WHERE IsActived = 1 AND ParentId = @ParentId";
            var result = await Connection.QueryAsync<Category>(sql, new { ParentId = parentId });
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<DataPaging<Category>?> FilterDataPaging(CategoryFilter filter)
    {
        try
        {
            filter.Page ??= 0;
            filter.Limit ??= 50;
            int offset = filter.Page.Value * filter.Limit.Value;
            filter.Offset = offset;
            var condition = new List<string>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                condition.Add("Name like '%@Name%'");
            }

            if (filter.IsActived != null)
            {
                condition.Add("IsActived = @IsActived");
            }

            var sqlCount = "SELECT COUNT(Id) FROM Category WHERE Id <> '' ";
            var sql = "SELECT * FROM Category WHERE Id <> '' ";

            if (condition.Any())
            {
                sqlCount += " AND " + $"{string.Join("AND", condition)}";
                sql += " AND " + $"{string.Join("AND", condition)}";
            }

            sql += " ORDER BY CreatedAt DESC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            var count = await CountBySql(sqlCount, filter);
            var data = await GetBySql(sql, filter);

            return new DataPaging<Category> { Data = data, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    private async Task<IEnumerable<Category>?> GetBySql(string sql, object filter)
    {
        try
        {
            var result = await Connection.QueryAsync<Category>(sql, filter, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return null;
        }
    }

    private async Task<long> CountBySql(string sql, object filter)
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
