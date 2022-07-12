using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBackend.Migrations
{
    public partial class ChangedOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PostalId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PostalId",
                table: "Orders",
                column: "PostalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders",
                column: "PostalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PostalId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PostalId",
                table: "Orders",
                column: "PostalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders",
                column: "PostalId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
