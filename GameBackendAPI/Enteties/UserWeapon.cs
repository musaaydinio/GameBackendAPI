using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameBackendAPI.Enteties
{
    // Oyuncu ve Silah arasındaki (many-to-many) ilişkiyi kurmak için oluşturduğum ara tablo.
    // Hangi oyuncunun envanterinde hangi silahların olduğunu bu tablo üzerinden takip ediyorum.
    public class UserWeapon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WeaponId { get; set; }
        public Weapon Weapon { get; set; }
    }
}
