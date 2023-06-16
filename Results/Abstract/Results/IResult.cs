namespace Results.Abstract.Results
{
    public interface IResult
    {
        bool Success { get; set; }

        string Message { get; set; }
    }
}
