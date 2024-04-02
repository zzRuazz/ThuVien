using Common.Abstractions;
using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Entities;
using System.Data;
using Models.Common;
using Models.Filter;
using Dapper;
using System.Text;

namespace WebThuVienAPI.Infrastructure.Implementations;

/// <inheritdoc/>
internal class ManufactureRepository : GenericRepository, IManufactureRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="connection"></param>
    /// <param name="logProvider"></param>
    public ManufactureRepository(IDbTransaction transaction, IDbConnection connection, ILogProvider logProvider) : base(transaction, connection, logProvider)
    {
    }

    /// <inheritdoc/>
    public async Task<int> CreateAsync(Manufacture entity)
    {
        try
        {
            var sql = @"INSERT INTO Manufacture
                            (Id, Name, Slug, Image, Website, IsActived, CreatedAt, IsDeleted)
                        VALUES
                            (@Id, @Name, @Slug, @Image, @Website, @IsActived, @CreatedAt, @IsDeleted);";

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
                var sql = @"DELETE FROM [Manufacture] WHERE [Id] = @Id";
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
    public async Task<DataPaging<Manufacture>?> FilterDataPaging(ManufactureFilter filter)
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

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                condition.Add("Slug like '%@Slug%'");
            }

            if (!string.IsNullOrEmpty(filter.Website))
            {
                condition.Add("Website like '%@Website%'");
            }

            if (filter.IsActived != null)
            {
                condition.Add("IsActived = @IsActived");
            }

            var sqlCount = "SELECT COUNT(Id) FROM Manufacture WHERE Id <> '' ";
            var sql = "SELECT * FROM Manufacture WHERE Id <> '' ";

            if (condition.Any())
            {
                sqlCount += " AND " + $"{string.Join("AND", condition)}";
                sql += " AND " + $"{string.Join("AND", condition)}";
            }

            sql += " ORDER BY CreatedAt DESC OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            var count = await CountBySql(sqlCount, filter);
            var data = await GetBySql(sql, filter);

            return new DataPaging<Manufacture> { Data = data, PaginationCount = count };
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Manufacture>?> GetAllAsync()
    {
        try
        {
            var sql = "SELECT * FROM Category ORDER BY CreatedAt DESC";
            var result = await Connection.QueryAsync<Manufacture>(sql);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<Manufacture?> GetAsync(string id)
    {
        try
        {
            var sql = "SELECT * FROM Manufacture WHERE Id = @Id";
            var result = await Connection.QueryFirstOrDefaultAsync<Manufacture>(sql, param: new { Id = id }, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Manufacture entity)
    {
        try
        {
            var find = await GetAsync(entity.Id);

            if (find != null)
            {
                var sbSQL = new StringBuilder();
                sbSQL.Append("UPDATE [Manufacture] SET ");

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

                if (entity.Image != find.Image)
                {
                    sbSQL.Append(" Image = @Image, ");
                }

                if (entity.Website != find.Website)
                {
                    sbSQL.Append(" Website = @Website, ");
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

    private async Task<IEnumerable<Manufacture>?> GetBySql(string sql, object filter)
    {
        try
        {
            var result = await Connection.QueryAsync<Manufacture>(sql, filter, transaction: Transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return null;
        }
    }
}
