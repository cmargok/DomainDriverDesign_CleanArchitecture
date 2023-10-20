namespace CleanArchitecture.Api.MIddlewares.Errors
{
    public class CodeErrorResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public CodeErrorResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }


        private string GetDefaultMessageStatusCode(int statusCode)
        {

            return statusCode switch
            {
                400 => "bad request",
                401 => "no autorizado",
                404 => "no encontrado ",
                500 => "errores en el servidor",
                _ => ""
            };

        }
    }
}
