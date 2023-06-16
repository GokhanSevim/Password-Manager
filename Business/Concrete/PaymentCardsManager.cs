using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
	public class PaymentCardsManager : IPaymentCardsService
	{
		private readonly IPaymentCardsDal _paymentCardsDal;
		private readonly ICategoryService _categoriesService;

		public PaymentCardsManager(IPaymentCardsDal paymentCardsDal, ICategoryService categoriesService)
		{
			_paymentCardsDal = paymentCardsDal;
			_categoriesService = categoriesService;
		}

		public async Task<IDataResult<EPaymentCards>> AddAsync(PaymentCardDto input)
		{
			var result = new DataResult<EPaymentCards>(null, false);

			if (input is null)
				return result;

			var entity = new EPaymentCards()
			{
				UserId = input.UserId,
				PasswordReprompt = input.PasswordReprompt,
				Name = input.Name,
				CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId),
				InitVect = AesCryptHelper.GenerateRandomBytes(16),
				CreationTime = DateTime.Now.ToUniversalTime()
			};

			entity.Note = await AesCryptHelper.EncryptStringToBytes(input.NoteTemp, input.SecretKey, entity.InitVect);
			entity.ExprationDate = await AesCryptHelper.EncryptStringToBytes(input.ExprationDateTemp, input.SecretKey, entity.InitVect);
			entity.CardNumber = await AesCryptHelper.EncryptStringToBytes(input.CardNumberTemp, input.SecretKey, entity.InitVect);
			entity.CardOwner = await AesCryptHelper.EncryptStringToBytes(input.CardOwnerTemp, input.SecretKey, entity.InitVect);
			entity.CardType = await AesCryptHelper.EncryptStringToBytes(input.CardTypeTemp, input.SecretKey, entity.InitVect);
			entity.SecurityCode = await AesCryptHelper.EncryptStringToBytes(input.SecurityCodeTemp, input.SecretKey, entity.InitVect);

			var insert = await _paymentCardsDal.AddAsync(entity);

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
			var delete = await _paymentCardsDal.DeleteAllAsync();

			return new Result(delete);
		}

		public async Task<IResult> DeleteAsync(Expression<Func<EPaymentCards, bool>> filter)
		{
			var delete = await _paymentCardsDal.DeleteAsync(filter);

			return new Result(delete);
		}

		public async Task<IDataResult<List<PaymentCardDto>>> FindAllAsync(byte[] secretKey, Expression<Func<EPaymentCards, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EPaymentCards>, IOrderedQueryable<EPaymentCards>> orderBy = null)
		{
			var dataList = await _paymentCardsDal.FindAllAsync(filter, Take, orderBy);
			var resultList = new List<PaymentCardDto>();

			foreach (var item in dataList)
			{
				resultList.Add(new PaymentCardDto()
				{
					Id = item.Id,
					UserId = item.UserId,
					CategoryId = item.CategoryId,
					PasswordReprompt = item.PasswordReprompt,
					Name = item.Name,
					CreationTime = item.CreationTime,
					CardNumberTemp = await AesCryptHelper.DecryptStringFromBytes(item.CardNumber, secretKey, item.InitVect),
					CardOwnerTemp = await AesCryptHelper.DecryptStringFromBytes(item.CardOwner, secretKey, item.InitVect),
					CardTypeTemp = await AesCryptHelper.DecryptStringFromBytes(item.CardType, secretKey, item.InitVect),
					ExprationDateTemp = await AesCryptHelper.DecryptStringFromBytes(item.ExprationDate, secretKey, item.InitVect),
					NoteTemp = await AesCryptHelper.DecryptStringFromBytes(item.Note, secretKey, item.InitVect),
					SecurityCodeTemp = await AesCryptHelper.DecryptStringFromBytes(item.SecurityCode, secretKey, item.InitVect)
				});
			}

			return new DataResult<List<PaymentCardDto>>(resultList, resultList is not null && resultList.Any());
		}

		public async Task<IDataResult<PaymentCardDto>> GetAsync(Expression<Func<EPaymentCards, bool>> filter, byte[] secretKey)
		{
			var result = await _paymentCardsDal.GetAsync(filter);

			var temp = new PaymentCardDto()
			{
				Id = result.Id,
				UserId = result.UserId,
				CategoryId = result.CategoryId,
				PasswordReprompt = result.PasswordReprompt,
				Name = result.Name,
				CreationTime = result.CreationTime,
				CardNumberTemp = await AesCryptHelper.DecryptStringFromBytes(result.CardNumber, secretKey, result.InitVect),
				CardOwnerTemp = await AesCryptHelper.DecryptStringFromBytes(result.CardOwner, secretKey, result.InitVect),
				CardTypeTemp = await AesCryptHelper.DecryptStringFromBytes(result.CardType, secretKey, result.InitVect),
				ExprationDateTemp = await AesCryptHelper.DecryptStringFromBytes(result.ExprationDate, secretKey, result.InitVect),
				NoteTemp = await AesCryptHelper.DecryptStringFromBytes(result.Note, secretKey, result.InitVect),
				SecurityCodeTemp = await AesCryptHelper.DecryptStringFromBytes(result.SecurityCode, secretKey, result.InitVect)
			};

			return new DataResult<PaymentCardDto>(temp, result.Id > 0);
		}

		public async Task<IDataResult<EPaymentCards>> UpdateAsync(PaymentCardDto input)
		{
			var result = new DataResult<EPaymentCards>(null, false);

			if (input is null || input.Id <= 0)
				return result;

			var row = await _paymentCardsDal.GetAsync(x => x.Id == input.Id);

			if (row is not null)
			{
				row.CategoryId = await _categoriesService.GetAndCheckCategoryId(input.CategoryTemp, input.UserId);
				row.Name = input.Name;
				row.PasswordReprompt = input.PasswordReprompt;

				row.CardNumber = await AesCryptHelper.EncryptStringToBytes(input.CardNumberTemp, input.SecretKey, row.InitVect);
				row.Note = await AesCryptHelper.EncryptStringToBytes(input.NoteTemp, input.SecretKey, row.InitVect);
				row.SecurityCode = await AesCryptHelper.EncryptStringToBytes(input.SecurityCodeTemp, input.SecretKey, row.InitVect);
				row.CardOwner = await AesCryptHelper.EncryptStringToBytes(input.CardOwnerTemp, input.SecretKey, row.InitVect);
				row.CardType = await AesCryptHelper.EncryptStringToBytes(input.CardTypeTemp, input.SecretKey, row.InitVect);
				row.ExprationDate = await AesCryptHelper.EncryptStringToBytes(input.ExprationDateTemp, input.SecretKey, row.InitVect);

				var update = await _paymentCardsDal.UpdateAsync(row);

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
