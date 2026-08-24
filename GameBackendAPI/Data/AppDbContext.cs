using GameBackendAPI.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameBackendAPI.Data
{
    // EF Core DbContext yapılandırması
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User>Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        // User ve Weapon arasındaki many-to-many ilişkiyi tutan ara tablo
        public DbSet<UserWeapon> UserWeapons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserWeapon ara tablosunun foreign key bağlantılarını kuruyoruz
            modelBuilder.Entity<UserWeapon>().HasOne(uw => uw.User)
                .WithMany(u => u.Inventory)
                .HasForeignKey(uw => uw.UserId);

            modelBuilder.Entity<UserWeapon>().HasOne(uw => uw.Weapon)
                .WithMany()
                .HasForeignKey(uw => uw.WeaponId);

            // Veritabanı ilk oluştuğunda markette yer alacak default silahları seed'liyoruz
            modelBuilder.Entity<Weapon>().HasData(
                new Weapon { Id = 1, Name = "Pistol", Price = 500 ,Damage = 10 },
                new Weapon { Id = 2, Name = "AK 47", Price = 5000, Damage =  22},
                new Weapon { Id = 3, Name = "ShotGun", Price = 4000, Damage = 45 },
                new Weapon { Id = 4, Name = "M468", Price = 5000, Damage = 22 },
                new Weapon { Id = 5, Name = "UMP-45", Price = 3000, Damage = 14 },
                new Weapon { Id = 6, Name = "Buldog", Price = 4500, Damage = 20 },
                new Weapon { Id = 7, Name = "Revolver", Price = 1500, Damage = 25 },
                new Weapon { Id = 8, Name = "ShotCannon", Price = 2500, Damage = 40 },
                new Weapon { Id = 9, Name = "SMG", Price = 1000, Damage = 12 },
                new Weapon { Id = 10, Name = "MP9", Price = 2500, Damage = 13 },
                new Weapon { Id = 11, Name = "Revolver-Small", Price = 1000, Damage = 18 },              
                new Weapon { Id = 12, Name = "AKS-74U", Price = 3000, Damage = 14 },
                new Weapon { Id = 13, Name = "MP7", Price = 2750, Damage = 12 },
                new Weapon { Id = 14, Name = "ShotGun-2", Price = 4000, Damage = 50 }
                );
        }
    }
}
