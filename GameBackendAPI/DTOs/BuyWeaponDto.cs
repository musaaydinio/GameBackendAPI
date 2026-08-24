namespace GameBackendAPI.DTOs
{
    // Marketten silah alım isteği atıldığında, sadece alınacak silahın ID'sini taşımak için kullandığım DTO
    public class BuyWeaponDto
    {
        public int WeaponId { get; set; }
    }
}
