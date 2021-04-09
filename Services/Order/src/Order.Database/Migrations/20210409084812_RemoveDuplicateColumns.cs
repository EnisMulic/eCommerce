using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.Database.Migrations
{
    public partial class RemoveDuplicateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Buyers_BuyerId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CardTypes_CardTypeId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Name1",
                table: "Buyers");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "PaymentMethods");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CardTypeId",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_CardTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BuyerId",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_BuyerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Buyers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Buyers_BuyerId",
                table: "PaymentMethods",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_CardTypes_CardTypeId",
                table: "PaymentMethods",
                column: "CardTypeId",
                principalTable: "CardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Buyers_BuyerId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_CardTypes_CardTypeId",
                table: "PaymentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_CardTypeId",
                table: "Payments",
                newName: "IX_Payments_CardTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_BuyerId",
                table: "Payments",
                newName: "IX_Payments_BuyerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Buyers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name1",
                table: "Buyers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Buyers_BuyerId",
                table: "Payments",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CardTypes_CardTypeId",
                table: "Payments",
                column: "CardTypeId",
                principalTable: "CardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
