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
	public interface IBankAccountsService
	{
		Task<IDataResult<BankAccountDto>> GetAsync(Expression<Func<EBankAccounts, bool>> filter, byte[] secretKey);

		Task<IDataResult<List<BankAccountDto>>> FindAllAsync(byte[] secretKey, Expression<Func<EBankAccounts, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EBankAccounts>, IOrderedQueryable<EBankAccounts>> orderBy = null);

		Task<IDataResult<EBankAccounts>> AddAsync(BankAccountDto input);

		Task<IDataResult<EBankAccounts>> UpdateAsync(BankAccountDto input);

		Task<IResult> DeleteAsync(Expression<Func<EBankAccounts, bool>> filter);

		Task<IResult> DeleteAllAsync();
	}
}
