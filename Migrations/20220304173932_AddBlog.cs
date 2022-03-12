using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace redux.Migrations
{
    public partial class AddBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Category",
                newName: "ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                newName: "IX_Category_ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "Category",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                newName: "IX_Category_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
