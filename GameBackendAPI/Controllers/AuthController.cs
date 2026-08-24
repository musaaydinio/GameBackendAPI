
using Azure.Core;
using GameBackendAPI.DTOs;
using GameBackendAPI.Services.Contract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        // Boş veya sadece boşluk tuşuna basılmış kayda izin verme.
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Kullanıcı adı ve şifre boş bırakılamaz!");

        var result = await _authService.Register(request);
        // Eğer başarısızsa direkt hatayı fırlat.
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        // Başarısızsa  BadRequest ile mesajı yolla.
        return Ok(result.Message);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        // Boş giriş denemelerini veritabanına bile sormadan kapıdan çevir.
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Kullanıcı adı ve şifre boş bırakılamaz!");
        // result artık (bool IsSuccess, string Message, string Token) paketi olarak dönüyor
        var result = await _authService.Login(request);

        // Eğer başarısızsa  direkt hatayı fırlat
        if (!result.IsSuccess)
            return Unauthorized(result.Message);

        // Eğer buraya indiyse Tokenı ver
        return Ok(new { Token = result.Token });
    }
}
