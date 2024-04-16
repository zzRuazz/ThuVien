using Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using WebThuVienAPI.Infrastructure.Abstractions;
using WebThuVienAPI.Models.Context;

namespace WebThuVienAPI.Infrastructure.Implementations;

public class RepositoryBase<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    protected readonly ILogProvider _logProvider;

    protected readonly IConfiguration _configuration;

    public RepositoryBase(ApplicationDbContext context, ILogProvider logProvider, IConfiguration configuration)
    {
        _context = context;
        _logProvider = logProvider;
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
            return string.Empty;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            T? entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<T>?> GetAllAsync()
    {
        try
        {
            var res = await _context.Set<T>().ToListAsync();
            return res;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync(string id)
    {
        try
        {
            T? entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            T? find = await _context.Set<T>().FindAsync(entity.Id);
            if (find != null)
            {
                _context.Set<T>().Update(find);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            _logProvider.Error(ex);
        }

        return false;
    }

    Task<int> IGenericRepository<T>.CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
