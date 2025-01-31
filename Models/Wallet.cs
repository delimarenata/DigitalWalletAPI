
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWalletAPI.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public required User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
