using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Filters;
using Microsoft.AspNetCore.Authorization;
using Results.Abstract.Results;
using Results.Concrete.Results;

namespace Business.Concrete
{
	[TryException]
	[AuthorizeWithSession]
	public class SmtpSettingsManager : ISmtpSettingsService
	{
		private readonly ISmtpSettingsDal _smtpSettings;

		public SmtpSettingsManager(ISmtpSettingsDal smtpSettings)
		{
			_smtpSettings = smtpSettings;
		}

		public async Task<IDataResult<ESmtpSettings>> AddAsync(ESmtpSettings entity)
		{
			var result = new DataResult<ESmtpSettings>(null, false);

			if (entity is null)
			{
				result.Message = "Geçersiz Veri";
				return result;
			}

			entity.CreationTime = DateTime.Now.ToUniversalTime();

			var insert = await _smtpSettings.AddAsync(entity);

			if (insert.Id > 0)
			{
				result.Data = insert;
				result.Message = "SMTP oluşturuldu.";
				result.Success = true;
			}

			return result;
		}

		public async Task<IDataResult<ESmtpSettings>> GetAsync()
		{
			var list = await _smtpSettings.FindAllAsync();

			var result = list.MaxBy(x => x.Id);

			return new DataResult<ESmtpSettings>(result, result.Id > 0);
		}

		public async Task<IDataResult<ESmtpSettings>> UpdateAsync(ESmtpSettings entity)
		{
			var result = new DataResult<ESmtpSettings>(null, false);

			var row = await _smtpSettings.GetAsync(x => x.Id == entity.Id);

			if (row is not null)
			{

				row.Host = entity.Host;
				row.Port = entity.Port;
				row.UseHTML = entity.UseHTML;
				row.UseDefaultCredentials = entity.UseDefaultCredentials;
				row.DefaultTitle = entity.DefaultTitle;
				row.From = entity.From;
				row.SenderName = entity.SenderName;
				row.Signature = entity.Signature;
				row.TimeOut = entity.TimeOut;
				row.Password = entity.Password;
				row.User = entity.User;
				row.UseSSL = entity.UseSSL;

				var update = await _smtpSettings.UpdateAsync(row);

				if (update.Id == entity.Id)
				{
					result.Message = "SMTP bilgisi güncellendi.";
					result.Success = true;
				}
			}

			return result;
		}
	}
}
