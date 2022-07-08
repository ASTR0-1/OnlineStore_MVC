using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore_DAL.Migrations
{
    public partial class ReceiptFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "Receipts",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_UserId",
                table: "Receipts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_UserId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Receipts",
                newName: "User");
        }
    }
}
