

namespace GameBackendAPI.DTOs
{
    // Oyuncunun sisteme giriş yaparken gönderdiği verileri tutan model
    public class LoginDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
