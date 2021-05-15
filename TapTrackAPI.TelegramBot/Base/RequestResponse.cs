namespace TapTrackAPI.TelegramBot.Base
{
    public class RequestResponse
    {
        public RequestResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
    }
}