using GameBackendAPI.DTOs;

namespace GameBackendAPI.Services.Contract
{
    public interface IAuthService
    {
        // Kayıt ve giriş işlemleri  (Task) çalışacak ve geriye token dönecek.
        Task<(bool IsSuccess, string Message, string Token)> Login(LoginDto request);
        Task<(bool IsSuccess, string Message)> Register(RegisterDto request);
    }
}
