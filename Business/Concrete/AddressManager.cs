using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
	public class AddressManager : IAddressService
	{
		private readonly IAddressesDal _addressesDal;
		private readonly ICategoryService _categoriesService;

		public AddressManager(IAddressesDal addressesDal, ICategoryService categoriesService)
		{
			_addressesDal = addressesDal;
			_categoriesService = categoriesService;
		}

		public async Task<IDataResult<EAddresses>> AddAsync(AddressDto input)
		{
			var result = new DataResult<EAddresses>(null, false);

			if (input is null)
				return result;

			var entity = new EAddresses()
			{
				Address = input.Address,
				CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId),
				UserId = input.UserId,
				Firstname = input.Firstname,
				Lastname = input.Lastname,
				Email = input.Email,
				Name = input.Name,
				Note = input.Note,
				PasswordReprompt = input.PasswordReprompt,
				CreationTime = DateTime.Now.ToUniversalTime()
			};

			var insert = await _addressesDal.AddAsync(entity);

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
			var delete = await _addressesDal.DeleteAllAsync();

			return new Result(delete);
		}

		public async Task<IResult> DeleteAsync(Expression<Func<EAddresses, bool>> filter)
		{
			var delete = await _addressesDal.DeleteAsync(filter);

			return new Result(delete);
		}

		public async Task<IDataResult<List<AddressDto>>> FindAllAsync(Expression<Func<EAddresses, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EAddresses>, IOrderedQueryable<EAddresses>> orderBy = null)
		{
			var dataList = await _addressesDal.FindAllAsync(filter, Take, orderBy);
			var resultList = new List<AddressDto>();

			foreach (var item in dataList)
			{
				resultList.Add(new AddressDto()
				{
					Id = item.Id,
					Address = item.Address,
					CategoryId = item.CategoryId,
					UserId = item.UserId,
					Firstname = item.Firstname,
					Lastname = item.Lastname,
					Email = item.Email,
					Name = item.Name,
					Note = item.Note,
					PasswordReprompt = item.PasswordReprompt,
					CreationTime = item.CreationTime
				});
			}

			return new DataResult<List<AddressDto>>(resultList, resultList is not null && resultList.Any());
		}

		public async Task<IDataResult<AddressDto>> GetAsync(Expression<Func<EAddresses, bool>> filter)
		{
			var result = await _addressesDal.GetAsync(filter);

			if (result?.Id > 0)
			{
				var entity = new AddressDto()
				{
					Id = result.Id,
					Address = result.Address,
					CategoryId = result.CategoryId,
					UserId = result.UserId,
					Firstname = result.Firstname,
					Lastname = result.Lastname,
					Email = result.Email,
					Name = result.Name,
					Note = result.Note,
					PasswordReprompt = result.PasswordReprompt,
					CreationTime = result.CreationTime
				};

				return new DataResult<AddressDto>(entity, result.Id > 0);
			}

			return new DataResult<AddressDto>(null, false);
		}

		public async Task<IDataResult<AddressDto>> UpdateAsync(AddressDto entity)
		{
			var result = new DataResult<AddressDto>(null, false);

			if (entity is null || entity.Id <= 0)
				return result;

			var row = await _addressesDal.GetAsync(x => x.Id == entity.Id);

			if (row is not null)
			{
				row.Address = entity.Address;
				row.CategoryId = await _categoriesService.GetAndCheckCategoryId(entity.CategoryTemp, entity.UserId);
				row.Firstname = entity.Firstname;
				row.Lastname = entity.Lastname;
				row.Email = entity.Email;
				row.Name = entity.Name;
				row.Note = entity.Note;
				row.PasswordReprompt = entity.PasswordReprompt;

				var update = await _addressesDal.UpdateAsync(row);

				if (update?.Id == entity.Id)
				{
					result.Success = true;
					result.Message = "";
				}
			}

			return result;
		}
	}
}
