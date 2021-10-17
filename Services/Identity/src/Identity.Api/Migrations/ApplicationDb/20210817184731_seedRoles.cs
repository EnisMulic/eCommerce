using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Api.Migrations.ApplicationDb
{
    public partial class seedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ddd09e4-58f2-411b-9c1c-7aad55ec9445", "8fddade1-2f1b-441b-af0d-00a46742144c", "Buyer", "BUYER" },
                    { "95f17f08-e388-418f-ba9d-da4e0a1e389d", "082ef100-8170-47a8-963e-1edf1c82d06c", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ddd09e4-58f2-411b-9c1c-7aad55ec9445");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95f17f08-e388-418f-ba9d-da4e0a1e389d");
        }
    }
}
