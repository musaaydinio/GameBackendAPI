using BCrypt.Net;
using GameBackendAPI.Data;
using GameBackendAPI.DTOs;
using GameBackendAPI.Enteties;
using GameBackendAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;

namespace GameBackendAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        // Dependency Injection ile DbContext  ve Configuration  içeri alınıyor.
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool IsSuccess, string Message, string Token)> Login(LoginDto request)
        {
            // 1. Kullanıcıyı veritabanında bul.
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (user == null)
                return (false, "Kullanıcı adı bulunamadı.", "");

            // 2. BCrypt İLE ŞİFRE KONTROLÜ 
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordCorrect)
                return (false, "Hatalı şifre.", "");

            // 3. Her şey doğruysa Token üret
            string token = CreateToken(user);

            return (true, "Giriş Başarılı", token);
        }

        public async Task<(bool IsSuccess, string Message)> Register(RegisterDto request)
        {
            // 1. Kullanıcı adı daha önce alınmış mı kontrolü
            if (await _context.Users.AnyAsync(n => n.UserName == request.UserName))
            {
                // Artık sadece string değil, Tuple (bool, string) formatında dönüyoruz
                return (false, "Bu kullanıcı adı zaten var.");
            }

            // 2. Şifreyi BCrypt ile güvenli hale getirme 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Yeni kullanıcıyı oluştur ve veritabanına ekle
            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                Coins = 3000
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // İşlem başarılıysa true ve başarı mesajı dönüyoruz
            return (true, "Kayıt başarılı! Giriş yapabilirsiniz");
        }
       

        private string CreateToken(User user)
        {
            // Token'ın içine oyuncunun ID'sini ve adını gizliyoruz ki, 
            // ileride marketten silah alırken "Kim alıyor?" diye veritabanına tekrar sormamak için.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            // appsettings.json'daki keyimizi alıyoruz.
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
                .GetSection("Jwt:Key").Value));
            // Şifreleme algoritması
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            // Token tüm özelliklerini ayarlıyoruz.
            var token = new JwtSecurityToken(
                 issuer : _configuration.GetSection("Jwt:Issuer").Value,
                 audience :_configuration.GetSection("Jwt:Audience").Value,
                 claims:claims,
                 expires:DateTime.Now.AddDays(1),
                 signingCredentials:creds
            );
            // Oluşturulan Tokenı (string) formatına çevirip geri dönüyoruz.
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
