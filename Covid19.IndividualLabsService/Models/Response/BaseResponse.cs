namespace Covid19.IndividualLabsService.Models.Response
{
    public record BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            Message = string.Empty; 
        }
        public BaseResponse(string? message)
        {
            Success = true;
            Message = message ?? string.Empty; 
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
