namespace DigitalWalletAPI.Models
{
    public class ApiUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Perfil
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}