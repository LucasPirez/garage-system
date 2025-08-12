using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkShops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FailedAttempts = table.Column<int>(type: "integer", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "text", nullable: true),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false),
                    WorkShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_WorkShops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string[]>(type: "text[]", nullable: false),
                    Email = table.Column<string[]>(type: "text[]", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Dni = table.Column<string>(type: "text", nullable: true),
                    WorkShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_WorkShops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Plate = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NotifycationSent = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Cause = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Budget = table.Column<double>(type: "double precision", nullable: false),
                    FinalAmount = table.Column<double>(type: "double precision", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleEntries_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleEntries_WorkShops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EFSparePart",
                columns: table => new
                {
                    EFRepairOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFSparePart", x => new { x.EFRepairOrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_EFSparePart_VehicleEntries_EFRepairOrderId",
                        column: x => x.EFRepairOrderId,
                        principalTable: "VehicleEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkShops",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 8, 5, 17, 26, 11, 118, DateTimeKind.Utc).AddTicks(4698), null, "Taller Jesus", null, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 8, 5, 17, 26, 11, 118, DateTimeKind.Utc).AddTicks(4725), null, "Taller Silvana", null, null }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Email", "FailedAttempts", "IsLocked", "Password", "ResetPasswordToken", "UpdatedAt", "WorkShopId" },
                values: new object[,]
                {
                    { new Guid("b764d3d2-3790-4448-9e18-43dbb000047d"), new DateTime(2025, 8, 5, 17, 26, 11, 349, DateTimeKind.Utc).AddTicks(2636), "email2@gmail.com", 0, false, "$2a$11$8wuF5A.wQguNh0AB4c/Qn.vT/rGdmIRYC4oNUSIb8ZoyUtLq9xmMa", null, null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("e5809ee5-491f-49c1-90fd-54b352143507"), new DateTime(2025, 8, 5, 17, 26, 11, 118, DateTimeKind.Utc).AddTicks(4846), "email@gmail.com", 0, false, "$2a$11$C5/4RLeiYsVJn6VjOVum3uu6n9SPUkHe4IthJiY9r7htr3Pkgm36O", null, null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedAt", "Dni", "Email", "FirstName", "LastName", "PhoneNumber", "UpdatedAt", "WorkShopId" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 8, 5, 17, 26, 11, 578, DateTimeKind.Utc).AddTicks(3599), null, new[] { "lucaspirez42@gmail.com" }, "Juan ", "Perez", new[] { "3424388239" }, null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 8, 5, 17, 26, 11, 578, DateTimeKind.Utc).AddTicks(3640), null, new string[0], "Maria ", "Lopez", new string[0], null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_WorkShopId",
                table: "Admins",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WorkShopId",
                table: "Customers",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEntries_VehicleId",
                table: "VehicleEntries",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEntries_WorkShopId",
                table: "VehicleEntries",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "EFSparePart");

            migrationBuilder.DropTable(
                name: "VehicleEntries");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "WorkShops");
        }
    }
}
