namespace Core.Utilities.Results.Abstract
{
    public abstract class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }

        protected Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        protected Result(bool success)
        {
            Success = success;
        }
    }
}