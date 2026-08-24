
using GameBackendAPI.DTOs;
using GameBackendAPI.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameBackendAPI.Controllers
{
    //[Authorize] filtresi ile bu controller altındaki tüm uç noktalara sadece
    // geçerli bir JWT Token'a sahip doğrulanmış istemcilerin erişmesi güvence altına alınmıştır.
    [Authorize]
    [Route("api/[controller]")]
    [ApiController()]
    public class MarketController :ControllerBase
    {
        private readonly IMarketService _marketService;

        // Dependency Injection ile servis katmanını içeri alıyorum.
        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }

        // Oyunda satın alınabilecek tüm silahların listesini getirir.
        [HttpGet("weapons")]
        public async Task<IActionResult> GetWeapons()
        {
            var weapons =await _marketService.GetWeaponsAsync();
            return Ok(weapons);
        }

        // Giriş yapan oyuncunun kendi envanterindeki silahları listeler.
        [HttpGet("inventory")]
        public async Task<IActionResult> GetInventory()
        {
            // Token üzerinden kullanıcının ID'sini alarak sadece ona ait envanteri çekiyorum.
            int userId = GetUserIdFromToken();
            var inventory=await _marketService.GetInventoryAsync(userId);
            return Ok(inventory);
        }

        // Oyuncunun profil bilgilerini (kullanıcı adı ve coin miktarını) döner.
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            int userId = GetUserIdFromToken();
           
            var userProfile = await _marketService.GetUserProfileAsync(userId);

            if (userProfile == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(new { userName = userProfile.UserName, coins = userProfile.Coins });
        }

        // Oyuncunun marketten silah satın almasını sağlar.
        [HttpPost("buy")]
        public async Task<IActionResult> BuyWeapon([FromBody]BuyWeaponDto resquest)
        {
            int userId= GetUserIdFromToken();
            var result=await _marketService.BuyWeaponAsync(userId,resquest);

            // Servisten gelen yanıta göre hata veya başarı durumunu HTTP kodlarıyla client'a iletiyorum.
            if (result.StartsWith("Geçersiz")||result.StartsWith("İşlem")||result.StartsWith("Yetersiz"))
                return BadRequest(result);

            return Ok(result);
        }

        // Oyuncunun oyun içinde görevlerden veya maçlardan kazandığı coin'leri hesabına ekler.
        [HttpPost("add-coins")]
        public async Task<IActionResult> AddCoins([FromBody] AddCoinDto request)
        {
           
            int userId = GetUserIdFromToken();
           
            var result = await _marketService.AddCoinsAsync(userId, request.EarnedCoins);
           
            if (result.StartsWith("Hata") || result.StartsWith("Kullanıcı"))
                return BadRequest(result);

            return Ok(new { message = result });
        }

        // JWT token içerisinden kullanıcının benzersiz ID'sini güvenli bir şekilde parse eder.
        private int GetUserIdFromToken()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}
