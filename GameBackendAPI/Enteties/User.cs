namespace GameBackendAPI.Enteties
{
    // Veritabanındaki oyuncuları temsil eden ana modelimiz.
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        // Güvenlik için şifreleri veritabanında açık metin olarak değil, şifrelenmiş (hash) şekilde tutuyorum.
        public string PasswordHash { get; set; } = string.Empty;
        public int Coins { get; set; } = 1000;

        // Oyuncunun satın aldığı silahları liste halinde tuttuğum envanter bağlantısı.
        public List<UserWeapon>Inventory { get; set; }=new List<UserWeapon>();
    }
}
