using Entities;
using Results.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ISmtpSettingsService 
	{
		Task<IDataResult<ESmtpSettings>> AddAsync(ESmtpSettings entity);

		Task<IDataResult<ESmtpSettings>> GetAsync();

		Task<IDataResult<ESmtpSettings>> UpdateAsync(ESmtpSettings entity);
	}
}
