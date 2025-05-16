using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "WorkShops",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "9e84eec6-9680-4082-85a4-afa1e3e7445b", "Taller Silvana" },
                    { "c3114d5e-01ce-49a8-a4d7-eea0be64536a", "Taller Jesuita" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Dni", "Email", "FirstName", "LastName", "PhoneNumber", "WorkShopId" },
                values: new object[,]
                {
                    { "5029c209-c333-4243-9861-a96b094c61cc", null, null, new[] { "lucaspirez42@gmail.com" }, "Juan ", "Perez", new[] { "3424388239" }, "c3114d5e-01ce-49a8-a4d7-eea0be64536a" },
                    { "8b0fbf2a-35c0-4a32-86f6-2d937443f7c8", null, null, new string[0], "Maria ", "Lopez", new string[0], "c3114d5e-01ce-49a8-a4d7-eea0be64536a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "5029c209-c333-4243-9861-a96b094c61cc");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "8b0fbf2a-35c0-4a32-86f6-2d937443f7c8");

            migrationBuilder.DeleteData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: "9e84eec6-9680-4082-85a4-afa1e3e7445b");

            migrationBuilder.DeleteData(
                table: "WorkShops",
                keyColumn: "Id",
                keyValue: "c3114d5e-01ce-49a8-a4d7-eea0be64536a");

            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
