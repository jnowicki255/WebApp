namespace WebApp.Contracts.Entities.Result
{
    public class OperationResult<T> : BaseOperationResult
    {
        public T Value { get; set; }

        public OperationResult(T content)
        {
            Value = content;
            Success = true;
        }

        public OperationResult(string errorMessage, string errorContext = null)
            : base(errorMessage, errorContext)
        { }

        public OperationResult(Exception ex)
            : base(ex)
        { }
    }
}
