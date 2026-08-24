using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class SilahGuncellemeleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Damage", "Price" },
                values: new object[] { 10, 500 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Damage", "Price" },
                values: new object[] { 22, 5000 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Damage", "Name" },
                values: new object[] { 45, "ShotGun" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "Damage", "Name", "Price" },
                values: new object[,]
                {
                    { 4, 22, "M468", 5000 },
                    { 5, 14, "UMP-45", 3000 },
                    { 6, 20, "Buldog", 4500 },
                    { 7, 25, "Revolver", 1500 },
                    { 8, 40, "ShotCannon", 2500 },
                    { 9, 12, "SMG", 1000 },
                    { 10, 12, "MP9", 2500 },
                    { 11, 18, "Revolver-Small", 1000 },
                    { 12, 50, "Rocket", 10000 },
                    { 13, 14, "AKS-74U", 3000 },
                    { 14, 13, "MP7", 2750 },
                    { 15, 50, "ShotGun-2", 4000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Damage", "Price" },
                values: new object[] { 22, 2000 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Damage", "Price" },
                values: new object[] { 25, 3000 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Damage", "Name" },
                values: new object[] { 50, "ShootGun" });
        }
    }
}
