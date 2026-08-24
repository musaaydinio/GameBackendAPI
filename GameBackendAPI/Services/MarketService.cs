using GameBackendAPI.Data;
using GameBackendAPI.DTOs;
using GameBackendAPI.Enteties;
using GameBackendAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace GameBackendAPI.Services
{
    public class MarketService : IMarketService
    {
        private readonly AppDbContext _context;

        // Constructor Injection: DbContext bağımlılığının içeri alınması.
        public MarketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> BuyWeaponAsync(int userId, BuyWeaponDto buyWeaponDto)
        {
            // 1.Data Integrity Kontrolü 
            // İlgili oyuncunun ve talep edilen silahın veritabanında doğrulanması.
            var user=await _context.Users.Include(n=>n.Inventory)
                .FirstOrDefaultAsync(n=>n.Id==userId);

            var weapon= await _context.Weapons.FirstOrDefaultAsync(m=>m.Id==buyWeaponDto.WeaponId);

            if (user == null || weapon == null)
                return "Gçersiz işlem:Sistemde oyuncu veya silah bulunamadı.";
            //Satın alım kontrolü
            if (user.Inventory.Any(nm => nm.WeaponId == buyWeaponDto.WeaponId))
                return "İşlem reddedildi:Bu sialaha zaten sahipsin";
            //Bakiye yeterlilik doğrulaması
            if(user.Coins<weapon.Price)
                return $"Yetersiz bakiye. Gereken: {weapon.Price}, Mevcut: {user.Coins}";
            //State Değişimi ve Kayıt İşlemi
            user.Coins -=weapon.Price;
            var userWeapon = new UserWeapon
            {
                UserId = user.Id,
                WeaponId = weapon.Id,
            };
            _context.UserWeapons.Add(userWeapon);
            // EF Core SaveChangesAsync metodu, bakiye düşüşünü ve envanter eklemesini 
            // varsayılan olarak tek bir Transaction içerisinde gerçekleştirir.
            // İşlemlerden biri başarısız olursa veri tutarsızlığını önlemek için geri alınır.
            await _context.SaveChangesAsync();
            return $"Satın alma başarılı. Güncel bakiye: {user.Coins}";
        }

        public async Task<List<Weapon>> GetWeaponsAsync()
        {
            // Market UI'ında sergilenecek temel silah verilerinin AsNoTracking ile performanslı çekilmesi.
            // Sadece Read-Only işlemi yapıldığı için EF Core'un ChangeTracker maliyeti ortadan kaldırıldı.
            return await _context.Weapons.AsNoTracking().ToListAsync();
        }

        public async Task<List<Weapon>> GetInventoryAsync(int userId)
        {
            // Kullanıcının sahip olduğu silahların UserWeapons ara tablosu üzerinden  izleme kapatılarak (AsNoTracking) getirilmesi.
            return await _context.UserWeapons.AsNoTracking()
                .Where(n => n.UserId == userId)
                .Select(nm=>nm.Weapon)
                .ToListAsync();
        }

        public async Task<UserProfileDto> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            // Eğer kullanıcı veritabanında yoksa null dön
            if (user == null)
                return null;

            // Kullanıcı varsa, bilgileri DTO'ya koyup güvenli şekilde Controller'a yolla
            return new UserProfileDto
            {
                UserName = user.UserName,
                Coins = user.Coins
            };
        }

        public async Task<string> AddCoinsAsync(int userId, int earnedCoins)
        {
            // Kullanıcıyı veritabanından bul
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return "Kullanıcı bulunamadı.";
            }

            // Gelen coin miktarını mevcut bakiyeye ekle
            user.Coins += earnedCoins;

            // EF Core ile veritabanına kaydet
            await _context.SaveChangesAsync();

            return $"Başarılı: {earnedCoins} coin hesaba eklendi. Yeni bakiye: {user.Coins}";
        }
    }
}
