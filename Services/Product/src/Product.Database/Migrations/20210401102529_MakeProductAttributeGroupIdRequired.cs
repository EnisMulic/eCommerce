using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.Database.Migrations
{
    public partial class MakeProductAttributeGroupIdRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductAttributeGroups_ProductAttributeGr~",
                table: "ProductAttributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductAttributeGroupId",
                table: "ProductAttributes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductAttributeGroups_ProductAttributeGr~",
                table: "ProductAttributes",
                column: "ProductAttributeGroupId",
                principalTable: "ProductAttributeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductAttributeGroups_ProductAttributeGr~",
                table: "ProductAttributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductAttributeGroupId",
                table: "ProductAttributes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductAttributeGroups_ProductAttributeGr~",
                table: "ProductAttributes",
                column: "ProductAttributeGroupId",
                principalTable: "ProductAttributeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
