
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWalletAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("SenderWalletId")]
        public int SenderWalletId { get; set; }

        [ForeignKey("ReceiverWalletId")]
        public int ReceiverWalletId { get; set; }

        public required Wallet SenderWallet { get; set; }

        public required Wallet ReceiverWallet { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required string Status { get; set; }
    }
}
