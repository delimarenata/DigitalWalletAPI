namespace DigitalWalletAPI.DTOs
{
    public class ApiUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Definir perfil do usuário da API
    }
}