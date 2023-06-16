using Core.Entities;

namespace PM.UI.Models
{
	public class ErrorViewModel : IViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}