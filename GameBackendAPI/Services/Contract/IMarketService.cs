using GameBackendAPI.DTOs;
using GameBackendAPI.Enteties;

namespace GameBackendAPI.Services.Contract
{
    // Market ve envanter işlemlerinin business logic soyutladığım servis arayüzü.
    // Controller tarafını temiz tutmak için tüm mantığı bu metodlara devrediyorum.
    public interface IMarketService
    {
        Task<List<Weapon>> GetWeaponsAsync();
        Task<List<Weapon>> GetInventoryAsync(int userId);
        Task<string>BuyWeaponAsync(int userId,BuyWeaponDto buyWeaponDto);
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<string> AddCoinsAsync(int userId, int earnedCoins);
    }
}
