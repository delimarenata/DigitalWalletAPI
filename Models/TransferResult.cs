namespace DigitalWalletAPI.Models
{
    public class TransferResult
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        public TransferResult(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}