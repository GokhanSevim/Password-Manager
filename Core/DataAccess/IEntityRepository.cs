using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
	public interface IEntityRepository<T> where T : class, IEntity, new()
	{
		Task<T> AddAsync(T entity);

		Task<T> GetAsync(Expression<Func<T, bool>> filter);

		Task<List<T>> FindAllAsync(Expression<Func<T, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

		Task<T> UpdateAsync(T entity);

		Task<bool> DeleteAsync(Expression<Func<T, bool>> filter);

		Task<bool> DeleteAllAsync();

	}
}
