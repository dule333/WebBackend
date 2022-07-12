using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBackend.Migrations
{
    public partial class PostalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_OrderDto_OrderDtoId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderDtoId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "OrderDtoId",
                table: "OrderProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders",
                column: "PostalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderDtoId",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderDtoId",
                table: "OrderProducts",
                column: "OrderDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_OrderDto_OrderDtoId",
                table: "OrderProducts",
                column: "OrderDtoId",
                principalTable: "OrderDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_PostalId",
                table: "Orders",
                column: "PostalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
