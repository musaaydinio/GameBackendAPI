namespace GameBackendAPI.DTOs
{
    // Yeni oyuncu kayıt işleminde, gereksiz verileri almamak için sadece istenen bilgileri tutan DTO
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } =string.Empty;
    }
}
