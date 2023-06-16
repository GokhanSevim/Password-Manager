using Entities;
using Results.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICategoryService
	{
		Task<IDataResult<ECategories>> GetAsync(Expression<Func<ECategories, bool>> filter);

		Task<IDataResult<List<ECategories>>> FindAllAsync(Expression<Func<ECategories, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<ECategories>, IOrderedQueryable<ECategories>> orderBy = null);

		Task<IDataResult<ECategories>> AddAsync(ECategories entity);

		Task<IDataResult<ECategories>> UpdateAsync(ECategories entity);

		Task<IResult> DeleteAsync(Expression<Func<ECategories, bool>> filter);

		Task<IResult> DeleteAllAsync();

		Task<int> GetAndCheckCategoryId(string categoryTemp, string userId);
	}
}
