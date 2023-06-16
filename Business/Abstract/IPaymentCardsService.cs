using Dto.Crypt;
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
	public interface IPaymentCardsService
	{
		Task<IDataResult<PaymentCardDto>> GetAsync(Expression<Func<EPaymentCards, bool>> filter, byte[] secretKey);

		Task<IDataResult<List<PaymentCardDto>>> FindAllAsync(byte[] secretKey, Expression<Func<EPaymentCards, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EPaymentCards>, IOrderedQueryable<EPaymentCards>> orderBy = null);

		Task<IDataResult<EPaymentCards>> AddAsync(PaymentCardDto input);

		Task<IDataResult<EPaymentCards>> UpdateAsync(PaymentCardDto input);

		Task<IResult> DeleteAsync(Expression<Func<EPaymentCards, bool>> filter);

		Task<IResult> DeleteAllAsync();
	}
}
