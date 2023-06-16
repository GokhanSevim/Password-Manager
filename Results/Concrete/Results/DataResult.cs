namespace Results.Concrete.Results
{
	public class DataResult<T> : Result, Abstract.Results.IDataResult<T>
	{
		public T Data { get; set; }

		public DataResult(T data) : base(default)
		{
			this.Data = data;
		}

		public DataResult(T data, bool success, string message) : base(success, message)
		{
			Data = data;
			Success = success;
			Message = message;
		}

		public DataResult(T data, bool success) : base(success)
		{
			Data = data;
			Success = success;
		}
	}
}
