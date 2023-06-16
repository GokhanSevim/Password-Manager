namespace Results.Abstract.Results
{
	public interface IDataResult<out T> : IResult
	{
		T Data { get; }
	}
}
