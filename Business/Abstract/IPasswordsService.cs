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
	public interface IPasswordsService
	{
		Task<IDataResult<PasswordDto>> GetAsync(Expression<Func<EPasswords, bool>> filter, byte[] secretKey);

		Task<IDataResult<List<PasswordDto>>> FindAllAsync(byte[] secretKey, Expression<Func<EPasswords, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EPasswords>, IOrderedQueryable<EPasswords>> orderBy = null);

		Task<IDataResult<EPasswords>> AddAsync(PasswordDto input);

		Task<IDataResult<EPasswords>> UpdateAsync(PasswordDto input);

		Task<IResult> DeleteAsync(Expression<Func<EPasswords, bool>> filter);

		Task<IResult> DeleteAllAsync();
	}
}
