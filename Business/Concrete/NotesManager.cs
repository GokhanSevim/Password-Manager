using Business.Abstract;
using DataAccess.Abstract;
using Dto.Crypt;
using Entities;
using Filters;
using Microsoft.AspNetCore.Authorization;
using Results.Abstract.Results;
using Results.Concrete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	[TryException]
	[AuthorizeWithSession]
	public class NotesManager : INotesService
	{
		private readonly INotesDal _notesDal;
		private readonly ICategoryService _categoriesService;

		public NotesManager(INotesDal notesDal, ICategoryService categoryService)
		{
			_notesDal = notesDal;
			_categoriesService = categoryService;
		}

		public async Task<IDataResult<NoteDto>> AddAsync(NoteDto input)
		{
			var result = new DataResult<NoteDto>(null, false);

			if (input is null)
				return result;

			var entity = new ENotes()
			{
				UserId = input.UserId,
				CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId),
				Content = input.Content,
				Name = input.Name,
				PasswordReprompt = input.PasswordReprompt,
				CreationTime = DateTime.Now.ToUniversalTime()
			};

			var insert = await _notesDal.AddAsync(entity);

			if (insert?.Id > 0)
			{
				input.Id = insert.Id;

				result.Data = input;
				result.Success = true;
				result.Message = "";
			}

			return result;
		}

		public async Task<IResult> DeleteAllAsync()
		{
			var delete = await _notesDal.DeleteAllAsync();

			return new Result(delete);
		}

		public async Task<IResult> DeleteAsync(Expression<Func<ENotes, bool>> filter)
		{
			var delete = await _notesDal.DeleteAsync(filter);

			return new Result(delete);
		}

		public async Task<IDataResult<List<NoteDto>>> FindAllAsync(Expression<Func<ENotes, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<ENotes>, IOrderedQueryable<ENotes>> orderBy = null)
		{
			var dataList = await _notesDal.FindAllAsync(filter, Take, orderBy);
			var resultList = new List<NoteDto>();

			foreach (var item in dataList)
			{
				resultList.Add(new NoteDto
				{
					Id = item.Id,
					Name = item.Name,
					CategoryId = item.CategoryId,
					Content = item.Content,
					CreationTime = item.CreationTime,
					PasswordReprompt = item.PasswordReprompt,
					UserId = item.UserId
				});
			}

			return new DataResult<List<NoteDto>>(resultList, resultList is not null && resultList.Any());
		}

		public async Task<IDataResult<NoteDto>> GetAsync(Expression<Func<ENotes, bool>> filter)
		{
			var result = await _notesDal.GetAsync(filter);

			if (result?.Id > 0)
			{
				var entity = new NoteDto()
				{
					UserId = result.UserId,
					CategoryId = result.CategoryId,
					Content = result.Content,
					Id = result.Id,
					Name = result.Name,
					PasswordReprompt = result.PasswordReprompt,
					CreationTime = result.CreationTime
				};

				return new DataResult<NoteDto>(entity, true);
			}

			return new DataResult<NoteDto>(null, false);
		}

		public async Task<IDataResult<NoteDto>> UpdateAsync(NoteDto input)
		{
			var result = new DataResult<NoteDto>(null, false);

			if (input is null || input.Id <= 0)
				return result;

			var row = await _notesDal.GetAsync(x => x.Id == input.Id && x.UserId == input.UserId);

			if (row is not null)
			{
				row.PasswordReprompt = input.PasswordReprompt;
				row.Content = input.Content;
				row.Name = input.Name;
				row.CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId);

				var update = await _notesDal.UpdateAsync(row);

				if (update?.Id == input.Id)
				{
					result.Success = true;
					result.Message = "";
				}
			}

			return result;
		}
	}
}
