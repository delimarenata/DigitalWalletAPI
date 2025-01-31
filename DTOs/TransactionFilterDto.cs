namespace DigitalWalletAPI.DTOs
{
    public class TransactionRequestDTO
    {
        public int SenderWalletId { get; set; }
        public int ReceiverWalletId { get; set; }
        public decimal Amount { get; set; }
    }
}

