using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "UpdatedAt", "WorkShopId" },
                values: new object[,]
                {
                    { new Guid("63418e00-38f8-4228-ac4b-d0a6a4a61e37"), new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2918), "email@gmail.com", "$2a$11$8kGJAGweY0hhIeCEmX7Rp.b1rRL5YBBF3r3Xo/ztzQ.oo4ZDj/fFa", null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("b559804b-a346-4d08-9456-02725d865b9c"), new DateTime(2025, 7, 8, 13, 55, 23, 632, DateTimeKind.Utc).AddTicks(3131), "email2@gmail.com", "$2a$11$ZkA4c24Xfu0mabG9FFh52ugt2UL2RA/QeQI/hMx6Fg918j1gEiHea", null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 13, 55, 23, 804, DateTimeKind.Utc).AddTicks(3438));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 13, 55, 23, 804, DateTimeKind.Utc).AddTicks(3482));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2747), "Taller Jesus" });

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2774));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("63418e00-38f8-4228-ac4b-d0a6a4a61e37"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("b559804b-a346-4d08-9456-02725d865b9c"));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 20, 5, 50, 995, DateTimeKind.Utc).AddTicks(4474));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 20, 5, 50, 995, DateTimeKind.Utc).AddTicks(4504));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2025, 6, 20, 20, 5, 50, 995, DateTimeKind.Utc).AddTicks(4207), "Taller Jesuita" });

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 20, 20, 5, 50, 995, DateTimeKind.Utc).AddTicks(4259));
        }
    }
}
