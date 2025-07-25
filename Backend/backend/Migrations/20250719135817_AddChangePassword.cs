using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddChangePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("63418e00-38f8-4228-ac4b-d0a6a4a61e37"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("b559804b-a346-4d08-9456-02725d865b9c"));

            migrationBuilder.AddColumn<int>(
                name: "FailedAttempts",
                table: "Admins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Admins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Admins",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Email", "FailedAttempts", "IsLocked", "Password", "ResetPasswordToken", "UpdatedAt", "WorkShopId" },
                values: new object[,]
                {
                    { new Guid("357eeb1f-9581-41ef-888e-7596b2adeae1"), new DateTime(2025, 7, 19, 13, 58, 14, 741, DateTimeKind.Utc).AddTicks(1397), "email@gmail.com", 0, false, "$2a$11$3dAzJKucroBwIvgoc/Vbj.mdUqNybdK6179ORRt8PVrLwfyIVwKmW", null, null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("a939c7ed-5d6b-4ed9-af6a-147d9b21f338"), new DateTime(2025, 7, 19, 13, 58, 14, 900, DateTimeKind.Utc).AddTicks(9834), "email2@gmail.com", 0, false, "$2a$11$8s3aAsaiwTcJb0vUQzOqVOJEKUlHkaDEU27nQNgT6PYL2kaDNUDBG", null, null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 19, 13, 58, 15, 59, DateTimeKind.Utc).AddTicks(7135));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 19, 13, 58, 15, 59, DateTimeKind.Utc).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 19, 13, 58, 14, 741, DateTimeKind.Utc).AddTicks(1234));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 19, 13, 58, 14, 741, DateTimeKind.Utc).AddTicks(1253));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("357eeb1f-9581-41ef-888e-7596b2adeae1"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("a939c7ed-5d6b-4ed9-af6a-147d9b21f338"));

            migrationBuilder.DropColumn(
                name: "FailedAttempts",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Admins");

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
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2747));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 8, 13, 55, 23, 461, DateTimeKind.Utc).AddTicks(2774));
        }
    }
}
