using Results.Abstract.Results;

namespace Results.Concrete.Results
{
	public class Result : IResult
	{
		public Result()
		{
		}

		public Result(bool success)
		{
			this.Success = success;
		}

		public Result(bool success, string message) : this(success)
		{
			this.Message = message;
		}

		public bool Success { get; set; }

		private string _Message;

		public string Message
		{
			get
			{
				if (string.IsNullOrEmpty(_Message))
				{
					return Success ? "İşleminiz gerçekleştirilmiştir." : "İşleminiz gerçekleştirilemedi.";
				}

				return _Message;
			}
			set { _Message = value; }
		}
	}
}
