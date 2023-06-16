using AngleSharp.Dom;
using Business.Abstract;
using DataAccess.Abstract;
using Dto.Crypt;
using Entities;
using Filters;
using Helpers;
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
	public class BankAccountsManager : IBankAccountsService
	{
		private readonly IBankAccountsDal _bankAccountsDal;
		private readonly ICategoryService _categoryService;

		public BankAccountsManager(IBankAccountsDal bankAccountsDal, ICategoryService categoryService)
		{
			_bankAccountsDal = bankAccountsDal;
			_categoryService = categoryService;
		}

		public async Task<IDataResult<EBankAccounts>> AddAsync(BankAccountDto input)
		{
			var result = new DataResult<EBankAccounts>(null, false);

			if (input is null || input.SecretKey is null)
				return result;

			var entity = new EBankAccounts()
			{
				UserId = input.UserId,
				CategoryId = await _categoryService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId),
				InitVect = AesCryptHelper.GenerateRandomBytes(16),
				Name = input.Name,
				PasswordReprompt = input.PasswordReprompt,
				CreationTime = DateTime.Now.ToUniversalTime()
			};

			entity.AccountNumber = await AesCryptHelper.EncryptStringToBytes(input.AccountNumberTemp, input.SecretKey, entity.InitVect);
			entity.SwiftCode = await AesCryptHelper.EncryptStringToBytes(input.SwiftCodeTemp, input.SecretKey, entity.InitVect);
			entity.Note = await AesCryptHelper.EncryptStringToBytes(input.NoteTemp, input.SecretKey, entity.InitVect);
			entity.Iban = await AesCryptHelper.EncryptStringToBytes(input.IbanTemp, input.SecretKey, entity.InitVect);
			entity.BankName = await AesCryptHelper.EncryptStringToBytes(input.BankNameTemp, input.SecretKey, entity.InitVect);

			var insert = await _bankAccountsDal.AddAsync(entity);

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
			var delete = await _bankAccountsDal.DeleteAllAsync();

			return new Result(delete);
		}

		public async Task<IResult> DeleteAsync(Expression<Func<EBankAccounts, bool>> filter)
		{
			var delete = await _bankAccountsDal.DeleteAsync(filter);

			return new Result(delete);
		}

		public async Task<IDataResult<List<BankAccountDto>>> FindAllAsync(byte[] secretKey, Expression<Func<EBankAccounts, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EBankAccounts>, IOrderedQueryable<EBankAccounts>> orderBy = null)
		{
			var dataList = await _bankAccountsDal.FindAllAsync(filter, Take, orderBy);
			var resultList = new List<BankAccountDto>();

			foreach (var item in dataList)
			{
				resultList.Add(new BankAccountDto()
				{
					Id = item.Id,
					UserId = item.UserId,
					CategoryId = item.CategoryId,
					Name = item.Name,
					AccountNumberTemp = await AesCryptHelper.DecryptStringFromBytes(item.AccountNumber, secretKey, item.InitVect),
					SwiftCodeTemp = await AesCryptHelper.DecryptStringFromBytes(item.SwiftCode, secretKey, item.InitVect),
					NoteTemp = await AesCryptHelper.DecryptStringFromBytes(item.Note, secretKey, item.InitVect),
					IbanTemp = await AesCryptHelper.DecryptStringFromBytes(item.Iban, secretKey, item.InitVect),
					BankNameTemp = await AesCryptHelper.DecryptStringFromBytes(item.BankName, secretKey, item.InitVect),
					PasswordReprompt = item.PasswordReprompt,
					CreationTime = item.CreationTime
				});
			}

			return new DataResult<List<BankAccountDto>>(resultList, resultList is not null && resultList.Any());
		}

		public async Task<IDataResult<BankAccountDto>> GetAsync(Expression<Func<EBankAccounts, bool>> filter, byte[] secretKey)
		{
			var result = await _bankAccountsDal.GetAsync(filter);

			if (result?.Id> 0)
			{
				var temp = new BankAccountDto() { 
				
					Id = result.Id,
					Name = result.Name,
					CategoryId = result.CategoryId,
					UserId = result.UserId,
					AccountNumberTemp = await AesCryptHelper.DecryptStringFromBytes(result.AccountNumber, secretKey, result.InitVect),
					IbanTemp = await AesCryptHelper.DecryptStringFromBytes(result.Iban, secretKey, result.InitVect),
					SwiftCodeTemp = await AesCryptHelper.DecryptStringFromBytes(result.SwiftCode, secretKey, result.InitVect),
					NoteTemp = await AesCryptHelper.DecryptStringFromBytes(result.Note, secretKey, result.InitVect),
					BankNameTemp = await AesCryptHelper.DecryptStringFromBytes(result.BankName, secretKey, result.InitVect),	
					PasswordReprompt = result.PasswordReprompt,
					CreationTime= result.CreationTime
				};

				return new DataResult<BankAccountDto>(temp, true);
			}

			return new DataResult<BankAccountDto>(null, false);
		}

		public async Task<IDataResult<EBankAccounts>> UpdateAsync(BankAccountDto input)
		{
			var result = new DataResult<EBankAccounts>(null, false);

			if (input is null || input.Id <= 0)
				return result;

			var row = await _bankAccountsDal.GetAsync(x => x.Id == input.Id);

			if (row is not null)
			{
				row.Name = input.Name;
				row.CategoryId = await _categoryService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId);
				row.UserId = input.UserId;
				row.PasswordReprompt = input.PasswordReprompt;

				row.AccountNumber = await AesCryptHelper.EncryptStringToBytes(input.AccountNumberTemp, input.SecretKey, row.InitVect);
				row.Iban = await AesCryptHelper.EncryptStringToBytes(input.IbanTemp, input.SecretKey, row.InitVect);
				row.SwiftCode = await AesCryptHelper.EncryptStringToBytes(input.SwiftCodeTemp, input.SecretKey, row.InitVect);
				row.Note = await AesCryptHelper.EncryptStringToBytes(input.NoteTemp, input.SecretKey, row.InitVect);
				row.BankName = await AesCryptHelper.EncryptStringToBytes(input.BankNameTemp, input.SecretKey, row.InitVect);

				var update = await _bankAccountsDal.UpdateAsync(row);

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
