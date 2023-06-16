using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
	public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
		where TEntity : class, IEntity, new()
		where TContext : DbContext, new()
	{
		public async Task<TEntity> AddAsync(TEntity entity)
		{
			using var context = new TContext();

			context.Entry(entity).State = EntityState.Added;

			await context.SaveChangesAsync();

			return entity;
		}

		public async Task<bool> DeleteAllAsync()
		{
			using var context = new TContext();

			var list = await context.Set<TEntity>().AsNoTracking().ToListAsync();

			context.Set<TEntity>().RemoveRange(list);

			return await context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter)
		{
			if (filter is null)
				return false;

			using var context = new TContext();

			var list = context.Set<TEntity>().Where(filter).AsNoTracking();

			if (list is null || !list.Any())
				return false;

			context.RemoveRange(list);

			return await context.SaveChangesAsync() > 0;
		}

		public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
		{
			using var context = new TContext();

			List<TEntity> list;

			if (filter is null)
			{
				if (orderBy is null)
				{
					list = await context.Set<TEntity>().AsNoTracking().Take(Take).ToListAsync();

				}
				else
				{
					list = await orderBy(context.Set<TEntity>().AsNoTracking().Take(Take)).ToListAsync();
				}
			}
			else
			{
				if (orderBy is null)
				{
					list = await context.Set<TEntity>().Where(filter).AsNoTracking().Take(Take).ToListAsync();
				}
				else
				{
					list = await orderBy(context.Set<TEntity>().Where(filter).AsNoTracking().Take(Take)).ToListAsync();
				}
			}

			return list ?? new List<TEntity>();
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
		{
			if (filter is null)
			{
				return new TEntity();
			}
			
			using var context = new TContext();

			var row = await context.Set<TEntity>().FirstOrDefaultAsync(filter);

			return row ?? new TEntity();
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			if (entity is null)
			{
				return new TEntity();
			}

			using var context = new TContext();

			context.Entry(entity).State = EntityState.Modified;

			await context.SaveChangesAsync();

			return entity;
		}
	}
}
