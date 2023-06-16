using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Filters;
using Microsoft.AspNetCore.Authorization;
using Results.Abstract.Results;
using Results.Concrete.Results;
using System.Linq.Expressions;
using Helpers;
using Dto.Crypt;

namespace Business.Concrete
{
	[TryException]
	[AuthorizeWithSession]
	public class PasswordsManager : IPasswordsService
	{
		private readonly IPasswordsDal _passwordsDal;
		private readonly ICategoryService _categoriesService;

		public PasswordsManager(IPasswordsDal passwordsDal, ICategoryService categoriesService)
		{
			_passwordsDal = passwordsDal;
			_categoriesService = categoriesService;
		}

		public async Task<IDataResult<EPasswords>> AddAsync(PasswordDto input)
		{
			var result = new DataResult<EPasswords>(null, false);

			if (input is null)
				return result;

			var entity = new EPasswords()
			{
				UserId = input.UserId,
				AutoLogin = input.AutoLogin,
				CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId),
				DisableAutofill = input.DisableAutofill,
				PasswordReprompt = input.PasswordReprompt,
				InitVect = AesCryptHelper.GenerateRandomBytes(16),
				CreationTime = DateTime.Now.ToUniversalTime()
			};

			entity.Url = await AesCryptHelper.EncryptStringToBytes(input.UrlTemp, input.SecretKey, entity.InitVect);
			entity.Name = await AesCryptHelper.EncryptStringToBytes(input.NameTemp, input.SecretKey, entity.InitVect);
			entity.UserName = await AesCryptHelper.EncryptStringToBytes(input.UserNameTemp, input.SecretKey, entity.InitVect);
			entity.Password = await AesCryptHelper.EncryptStringToBytes(input.PasswordTemp, input.SecretKey, entity.InitVect);
			entity.Note = await AesCryptHelper.EncryptStringToBytes(input.NoteTemp, input.SecretKey, entity.InitVect);

			var insert = await _passwordsDal.AddAsync(entity);

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
			var delete = await _passwordsDal.DeleteAllAsync();

			return new Result(delete);
		}

		public async Task<IResult> DeleteAsync(Expression<Func<EPasswords, bool>> filter)
		{
			var delete = await _passwordsDal.DeleteAsync(filter);

			return new Result(delete);
		}

		public async Task<IDataResult<List<PasswordDto>>> FindAllAsync(byte[] secretKey, Expression<Func<EPasswords, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EPasswords>, IOrderedQueryable<EPasswords>> orderBy = null)
		{
			var dataList = await _passwordsDal.FindAllAsync(filter, Take, orderBy);
			var resultList = new List<PasswordDto>();

			foreach (var item in dataList)
			{
				resultList.Add(new PasswordDto
				{
					Id = item.Id,
					UserId = item.UserId,
					AutoLogin = item.AutoLogin,
					CategoryId = item.CategoryId,
					CreationTime = item.CreationTime,
					DisableAutofill = item.DisableAutofill,
					PasswordReprompt = item.PasswordReprompt,
					UrlTemp = await AesCryptHelper.DecryptStringFromBytes(item.Url, secretKey, item.InitVect),
					NameTemp = await AesCryptHelper.DecryptStringFromBytes(item.Name, secretKey, item.InitVect),
					UserNameTemp = await AesCryptHelper.DecryptStringFromBytes(item.UserName, secretKey, item.InitVect),
					PasswordTemp = await AesCryptHelper.DecryptStringFromBytes(item.Password, secretKey, item.InitVect),
					NoteTemp = await AesCryptHelper.DecryptStringFromBytes(item.Note, secretKey, item.InitVect)
				});
			}

			return new DataResult<List<PasswordDto>>(resultList, resultList is not null && resultList.Any());
		}

		public async Task<IDataResult<PasswordDto>> GetAsync(Expression<Func<EPasswords, bool>> filter, byte[] secretKey)
		{
			var result = await _passwordsDal.GetAsync(filter);

			var temp = new PasswordDto
			{
				Id = result.Id,
				UserId = result.UserId,
				AutoLogin = result.AutoLogin,
				CategoryId = result.CategoryId,
				CreationTime = result.CreationTime,
				DisableAutofill = result.DisableAutofill,
				PasswordReprompt = result.PasswordReprompt,
				UrlTemp = await AesCryptHelper.DecryptStringFromBytes(result.Url, secretKey, result.InitVect),
				NameTemp = await AesCryptHelper.DecryptStringFromBytes(result.Name, secretKey, result.InitVect),
				UserNameTemp = await AesCryptHelper.DecryptStringFromBytes(result.UserName, secretKey, result.InitVect),
				PasswordTemp = await AesCryptHelper.DecryptStringFromBytes(result.Password, secretKey, result.InitVect),
				NoteTemp = await AesCryptHelper.DecryptStringFromBytes(result.Note, secretKey, result.InitVect)
			};

			return new DataResult<PasswordDto>(temp, result.Id > 0);
		}

		public async Task<IDataResult<EPasswords>> UpdateAsync(PasswordDto input)
		{
			var result = new DataResult<EPasswords>(null, false);

			if (input is null || input.Id <= 0)
				return result;

			var row = await _passwordsDal.GetAsync(x => x.Id == input.Id && x.UserId == input.UserId);

			if (row is not null)
			{
				row.AutoLogin = input.AutoLogin;
				row.CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId);
				row.DisableAutofill = input.DisableAutofill;
				row.PasswordReprompt = input.PasswordReprompt;

				row.Url = await AesCryptHelper.EncryptStringToBytes(input.UrlTemp, input.SecretKey, row.InitVect);
				row.Name = await AesCryptHelper.EncryptStringToBytes(input.NameTemp, input.SecretKey, row.InitVect);
				row.UserName = await AesCryptHelper.EncryptStringToBytes(input.UserNameTemp, input.SecretKey, row.InitVect);
				row.Password = await AesCryptHelper.EncryptStringToBytes(input.PasswordTemp, input.SecretKey, row.InitVect);
				row.Note = await AesCryptHelper.EncryptStringToBytes(input.NoteTemp, input.SecretKey, row.InitVect);

				var update = await _passwordsDal.UpdateAsync(row);

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
