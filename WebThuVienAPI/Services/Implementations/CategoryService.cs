using WebThuVienAPI.Infrastructure.Abstractions;
using Models.Common;
using Models.Entities;
using Models.Filter;
using Models.ViewModels;
using WebThuVienAPI.Services.Abstractions;

namespace WebThuVienAPI.Services.Implementations;

/// <inheritdoc/>
internal class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(Category entity)
    {
        var createResult = await _categoryRepository.CreateAsync(entity);
        return createResult;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string id)
    {
        var deleteResult = await _categoryRepository.DeleteAsync(id);
        return deleteResult;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Category>?> GetAllAsync()
    {
        var getAllResult = await _categoryRepository.GetAllAsync();
        return getAllResult;
    }

    /// <inheritdoc/>
    public async Task<Category?> GetAsync(string id)
    {
        var getOneResult = await _categoryRepository.GetAsync(id);
        return getOneResult;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Category entity)
    {
        var updateResult = await _categoryRepository.UpdateAsync(entity);
        return updateResult;
    }

    public async Task<IEnumerable<CategoryViewModel>?> GetShowingCategories()
    {
        var result = new List<CategoryViewModel>();
        var categories = await _categoryRepository.GetActiveParentCategories();

        if (categories != null && categories.Any())
        {
            foreach (var category in categories)
            {
                var categoryViewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Image = category.Image,
                    ParentId = category.ParentId,
                    Slug = category.Slug
                };

                var children = await _categoryRepository.GetActiveChildrenCategoriesByParentId(category.Id);

                if (children != null && children.Any())
                {
                    foreach (var child in children)
                    {
                        var childViewModel = new CategoryViewModel
                        {
                            Id = child.Id,
                            Name = child.Name,
                            Image = child.Image,
                            ParentId = child.ParentId,
                            Slug = child.Slug
                        };

                        result.Add(childViewModel);
                    }
                }

                result.Add(categoryViewModel);
            }
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryViewModel>?> GetActiveCategories()
    {
        var result = new List<CategoryViewModel>();
        var categories = await _categoryRepository.GetActiveParentCategories();

        if (categories != null && categories.Any())
        {
            foreach (var category in categories)
            {
                var categoryViewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Image = category.Image,
                    ParentId = category.ParentId,
                    Slug = category.Slug
                };

                var children = await _categoryRepository.GetActiveChildrenCategoriesByParentId(category.Id);

                if (children != null && children.Any())
                {
                    var childrenViewModel = new List<CategoryViewModel>();

                    foreach (var child in children)
                    {
                        var childViewModel = new CategoryViewModel
                        {
                            Id = child.Id,
                            Name = child.Name,
                            Image = child.Image,
                            ParentId = child.ParentId,
                            Slug = child.Slug
                        };

                        var subChildren = await _categoryRepository.GetActiveChildrenCategoriesByParentId(childViewModel.Id);

                        if (subChildren != null && subChildren.Any())
                        {
                            var subChildViewModelList = new List<CategoryViewModel>();

                            foreach (var subChild in subChildren)
                            {
                                var subChildViewModel = new CategoryViewModel
                                {
                                    Id = subChild.Id,
                                    Name = subChild.Name,
                                    Image = subChild.Image,
                                    ParentId = subChild.ParentId,
                                    Slug = subChild.Slug
                                };

                                subChildViewModelList.Add(subChildViewModel);
                            }

                            childViewModel.Children = subChildViewModelList;
                        }

                        childrenViewModel.Add(childViewModel);
                    }

                    categoryViewModel.Children = childrenViewModel;
                }

                result.Add(categoryViewModel);
            }
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<DataPaging<Category>?> FilterDataPaging(CategoryFilter filter)
    {
        var result = await _categoryRepository.FilterDataPaging(filter);
        return result;
    }
}
