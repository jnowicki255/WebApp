namespace WebApp.Contracts.Entities.Result
{
    public class Error
    {
        public string Message { get; set; }
        public string Context { get; set; }

        public Error() { }

        public Error(string message, string context)
        {
            Message = message;
            Context = context;
        }
    }
}
