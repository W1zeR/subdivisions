namespace WebApi.Responses
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public required string Message { get; set; }
        public IEnumerable<Error>? Errors { get; set; }
    }
}
