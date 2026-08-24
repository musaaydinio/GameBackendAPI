namespace GameBackendAPI.Enteties
{
    public class Weapon
    {
        // Oyundaki market sisteminde yer alan silahların temel özelliklerini tuttuğum model.
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Price { get; set; }
        public int Damage { get; set; }
    }
}
