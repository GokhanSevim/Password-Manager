using Results.Abstract.Results;

namespace Notification.Abstract
{
	public interface IEMailServices
	{
		Task<IResult> SendAsync(string Subject, string Body, string Recipients, string SenderName = "");

		Task<IResult> TestEmail(string Email);
	}
}