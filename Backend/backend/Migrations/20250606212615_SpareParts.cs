using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SpareParts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpareParts",
                table: "VehicleEntries");

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    VehicleEntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => new { x.VehicleEntryId, x.Id });
                    table.ForeignKey(
                        name: "FK_SparePart_VehicleEntries_VehicleEntryId",
                        column: x => x.VehicleEntryId,
                        principalTable: "VehicleEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 6, 21, 26, 14, 454, DateTimeKind.Utc).AddTicks(4));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 6, 21, 26, 14, 454, DateTimeKind.Utc).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 6, 21, 26, 14, 453, DateTimeKind.Utc).AddTicks(9906));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 6, 21, 26, 14, 453, DateTimeKind.Utc).AddTicks(9927));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SparePart");

            migrationBuilder.AddColumn<string[]>(
                name: "SpareParts",
                table: "VehicleEntries",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 31, 10, 407, DateTimeKind.Utc).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 31, 10, 407, DateTimeKind.Utc).AddTicks(2996));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 31, 10, 407, DateTimeKind.Utc).AddTicks(2849));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 31, 10, 407, DateTimeKind.Utc).AddTicks(2877));
        }
    }
}
