namespace GameBackendAPI.DTOs
{
    // Kullanıcının profil bilgilerini (sadece adını ve mevcut parasını) client tarafına güvenle dönmek için kullandığım DTO
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public int Coins { get; set; }
    }
}
