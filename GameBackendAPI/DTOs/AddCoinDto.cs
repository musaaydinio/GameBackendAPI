namespace GameBackendAPI.DTOs
{
    // Oyun sonu kazanılan veya görevlerden gelen coinleri backend'e iletirken kullandığım DTO
    public class AddCoinDto
    {       
        public int EarnedCoins { get; set; }
    }
}
