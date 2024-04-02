﻿using Models.Entities;

namespace WebThuVienAPI.Infrastructure.Abstractions;

/// <summary>
/// IGenericRepository
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// CreateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> CreateAsync(T entity);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetAsync(string id);

    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>?> GetAllAsync();

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string id);
}
