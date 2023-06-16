using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Filters;
using Results.Abstract.Results;
using Results.Concrete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Business.Concrete
{
	[TryException]
	[AuthorizeWithSession]
	public class CategoryManager : ICategoryService
	{
		private readonly ICategoriesDal _categoriesDal;

		public CategoryManager(ICategoriesDal categoriesDal)
		{
			_categoriesDal = categoriesDal;
		}

		public async Task<IDataResult<ECategories>> AddAsync(ECategories entity)
		{
			var result = new DataResult<ECategories>(null, false);

			if (entity is null)
				return result;

			entity.CreationTime = DateTime.Now.ToUniversalTime();

			var insert = await _categoriesDal.AddAsync(entity);

			if (insert?.Id > 0)
			{
				result.Data = insert;
				result.Success = true;
				result.Message = "";
			}

			return result;
		}

		public async Task<IResult> DeleteAllAsync()
		{
			var delete = await _categoriesDal.DeleteAllAsync();

			return new Result(delete);
		}

		public async Task<IResult> DeleteAsync(Expression<Func<ECategories, bool>> filter)
		{
			var delete = await _categoriesDal.DeleteAsync(filter);

			return new Result(delete);
		}

		public async Task<IDataResult<List<ECategories>>> FindAllAsync(Expression<Func<ECategories, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<ECategories>, IOrderedQueryable<ECategories>> orderBy = null)
		{
			var list = await _categoriesDal.FindAllAsync(filter, Take, orderBy);

			return new DataResult<List<ECategories>>(list, list is not null && list.Any());
		}

		public async Task<IDataResult<ECategories>> GetAsync(Expression<Func<ECategories, bool>> filter)
		{
			var result = await _categoriesDal.GetAsync(filter);

			return new DataResult<ECategories>(result, result.Id > 0);
		}

		public async Task<IDataResult<ECategories>> UpdateAsync(ECategories entity)
		{
			var result = new DataResult<ECategories>(null, false);

			if (entity is null || entity.Id <= 0)
				return result;

			var row = await _categoriesDal.GetAsync(x => x.Id == entity.Id);

			if (row is not null)
			{
				row.DisplayOrder = entity.DisplayOrder;
				row.Published = entity.Published;
				row.Description = entity.Description;
				row.Name = entity.Name;

				var update = await _categoriesDal.UpdateAsync(row);

				if (update?.Id == entity.Id)
				{
					result.Success = true;
					result.Message = "";
				}
			}

			return result;
		}

		public async Task<int> GetAndCheckCategoryId(string categoryTemp, string userId)
		{
			int CategoryId = 1;

			if (!string.IsNullOrWhiteSpace(categoryTemp))
			{
				if (int.TryParse(categoryTemp, out int categoryTempId))
				{
					switch (categoryTempId)
					{
						case 0:
							var getDefaultCategory = await _categoriesDal.GetAsync(x => x.IsDefault && x.UserId == userId);

							if (getDefaultCategory?.Id > 0)
							{
								CategoryId = getDefaultCategory.Id;
							}
							break;
						default:

							var getCategory = await _categoriesDal.GetAsync(x => x.Id == categoryTempId && x.UserId == userId);

							if (getCategory?.Id > 0)
							{
								CategoryId = categoryTempId;
							}
							else
							{
								var categoryEntity = new ECategories()
								{
									UserId = userId,
									Name = categoryTemp,
									Published = true,
									IsDefault = false
								};

								var category = await _categoriesDal.AddAsync(categoryEntity);

								if (category?.Id > 0)
								{
									CategoryId = category.Id;
								}
							}
							break;
					}
				}
				else
				{

					var getCategory = await _categoriesDal.GetAsync(x => x.Name == categoryTemp && x.UserId == userId);

					if (getCategory?.Id > 0)
					{
						CategoryId = categoryTempId;
					}
					else
					{
						var categoryEntity = new ECategories()
						{
							UserId = userId,
							Name = categoryTemp,
							IsDefault = false,
							Published = true
						};

						var category = await _categoriesDal.AddAsync(categoryEntity);

						if (category?.Id > 0)
						{
							CategoryId = category.Id;
						}
					}
				}
			}

			return CategoryId;
		}
	}
}
