using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class _SilahGüncellemsi_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Damage", "Name", "Price" },
                values: new object[] { 14, "AKS-74U", 3000 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Damage", "Name", "Price" },
                values: new object[] { 13, "MP7", 2750 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Damage", "Name", "Price" },
                values: new object[] { 50, "ShotGun-2", 4000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Damage", "Name", "Price" },
                values: new object[] { 50, "Rocket", 10000 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Damage", "Name", "Price" },
                values: new object[] { 14, "AKS-74U", 3000 });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Damage", "Name", "Price" },
                values: new object[] { 13, "MP7", 2750 });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "Damage", "Name", "Price" },
                values: new object[] { 15, 50, "ShotGun-2", 4000 });
        }
    }
}
