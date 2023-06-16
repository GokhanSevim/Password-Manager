using Dto.Account;
using Results.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAccountService
	{
		Task<IDataResult<string>> AddAsync(RegisterDto Input);

		Task<IDataResult<string>> LoginAsync(LoginDto Input);

		Task<IDataResult<string>> ForgotPasswordAsync(ForgotPasswordDto Input);

		Task<IDataResult<string>> ResetPasswordAsync(ResetPasswordDto Input);
	}
}
