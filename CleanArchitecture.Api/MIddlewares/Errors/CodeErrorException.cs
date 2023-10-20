namespace CleanArchitecture.Api.MIddlewares.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string Details { get; set; } = string.Empty;
        public CodeErrorException(int statusCode, string? message = null, string details = null!) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
