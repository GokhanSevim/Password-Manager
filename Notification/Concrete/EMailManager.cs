using DataAccess.Abstract;
using Filters;
using Notification.Abstract;
using System.Net;
using System.Net.Mail;
using Results.Abstract.Results;
using Results.Concrete.Results;
using Utilities;

namespace Notification.Concrete
{
	public class EMailManager : IEMailServices
	{
		private readonly ISmtpSettingsDal _smtpSettingsDal;

		public EMailManager(ISmtpSettingsDal smtpSettingsDal)
		{
			_smtpSettingsDal = smtpSettingsDal;
		}

		[TryException]
		public async Task<IResult> SendAsync(string Subject, string Body, string Recipients, string SenderName = "")
		{
			var result = new Result(false);

			var row = (await _smtpSettingsDal.FindAllAsync(Take: 1, orderBy: x => x.OrderByDescending(x => x.Id))).FirstOrDefault();

			if (row is not null && !string.IsNullOrEmpty(Recipients))
			{
				if (row.User == "your@email.net")
				{
					return new Result(true);
				}

				Subject = Utilities.Formats.Sanitize(Subject);
				Body = Utilities.Formats.SanitizeHTML(Body);

				var SMTP = new SmtpClient
				{
					TargetName = row.DefaultTitle,
					Port = row.Port,
					Host = row.Host ?? "",
					EnableSsl = row.UseSSL,
					UseDefaultCredentials = row.UseDefaultCredentials,
					Timeout = row.TimeOut,
					DeliveryFormat = SmtpDeliveryFormat.International,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					Credentials = new NetworkCredential(row.User, row.Password)
				};

				var Message = new MailMessage
				{
					From = new MailAddress(row.From ?? "", string.IsNullOrEmpty(SenderName) ? row.SenderName : SenderName),
					Subject = string.IsNullOrEmpty(Subject) ? row.DefaultTitle : Subject,
					IsBodyHtml = row.UseHTML,
					Body = $"{Body}<br><br>{row.Signature}<br>"
				};

				var ToMailList = Recipients.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();

				if (ToMailList?.Length > 0)
				{
					for (int i = 0; i < ToMailList.Length; i++)
					{
						string M = ToMailList.ElementAt(i).ToEmail();

						if (M.IsEmail())
						{
							Message.To.Add(new MailAddress(M));
						}
					}

					if (Message.To?.Count > 0)
					{
						//Mail ayarlarından kaynaklı, istisnai durumlarda smtp.send methodu yanıt vermeyi durdurabiliyor.
						//Task işlemine timeout koyarak bu durumun önüne geçebiliyoruz.
						Task.WaitAny(new Task[]
						{
							Task.Run(()=> {
								try
								{
									SMTP.Send(Message);
									result.Success = true;
								}
								catch (Exception ex)
								{
									result.Message = ex.Message;
									result.Success = false;
								}
							})

						}, row.TimeOut);

						if (result.Success)
						{
							result.Message = "E-posta başarıyla gönderildi.";
						}
						else
						{
							result.Message = "Mail gönderilirken bilinmeyen bir hata oluştu.";
						}
					}
				}
			}

			return result;
		}

		[TryException]
		public async Task<IResult> TestEmail(string Email)
		{
			var result = new Result(false);

			if (Email.IsEmail())
			{
				var send = await SendAsync("E-posta işlevselliği test ediliyor.", "E-posta iyi çalışıyor.", Email);

				result.Message = send.Message;
				result.Success = send.Success;
			}
			else
			{
				result.Message = "Geçersiz e-posta adresi";
			}

			return result;
		}
	}
}