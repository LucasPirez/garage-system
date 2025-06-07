using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SparePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpareParts",
                table: "VehicleEntries");

            migrationBuilder.RenameColumn(
                name: "Presupuest",
                table: "VehicleEntries",
                newName: "Budget");

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    RepairOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => new { x.RepairOrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_SparePart_VehicleEntries_RepairOrderId",
                        column: x => x.RepairOrderId,
                        principalTable: "VehicleEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 7, 22, 0, 11, 89, DateTimeKind.Utc).AddTicks(4859));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 7, 22, 0, 11, 89, DateTimeKind.Utc).AddTicks(4883));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 7, 22, 0, 11, 89, DateTimeKind.Utc).AddTicks(4703));

            migrationBuilder.UpdateData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 7, 22, 0, 11, 89, DateTimeKind.Utc).AddTicks(4734));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SparePart");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "VehicleEntries",
                newName: "Presupuest");

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
