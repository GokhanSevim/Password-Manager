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
	public interface IAddressService
	{
		Task<IDataResult<AddressDto>> GetAsync(Expression<Func<EAddresses, bool>> filter);

		Task<IDataResult<List<AddressDto>>> FindAllAsync(Expression<Func<EAddresses, bool>> filter = null, int Take = int.MaxValue, Func<IQueryable<EAddresses>, IOrderedQueryable<EAddresses>> orderBy = null);

		Task<IDataResult<EAddresses>> AddAsync(AddressDto input);

		Task<IDataResult<AddressDto>> UpdateAsync(AddressDto entity);

		Task<IResult> DeleteAsync(Expression<Func<EAddresses, bool>> filter);

		Task<IResult> DeleteAllAsync();
	}
}
