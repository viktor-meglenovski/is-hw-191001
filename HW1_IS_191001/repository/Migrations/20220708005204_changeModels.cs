using Microsoft.EntityFrameworkCore.Migrations;

namespace repository.Migrations
{
    public partial class changeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectionOrder_Order_OrderId",
                table: "ProjectionOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectionOrder_Projections_ProjectionId",
                table: "ProjectionOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectionOrder",
                table: "ProjectionOrder");

            migrationBuilder.RenameTable(
                name: "ProjectionOrder",
                newName: "ProjectionOrders");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectionOrder_ProjectionId",
                table: "ProjectionOrders",
                newName: "IX_ProjectionOrders_ProjectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectionOrder_OrderId",
                table: "ProjectionOrders",
                newName: "IX_ProjectionOrders_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCarts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectionOrders",
                table: "ProjectionOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId",
                table: "Projections",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectionOrders_Order_OrderId",
                table: "ProjectionOrders",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectionOrders_Projections_ProjectionId",
                table: "ProjectionOrders",
                column: "ProjectionId",
                principalTable: "Projections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_Movies_MovieId",
                table: "Projections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectionOrders_Order_OrderId",
                table: "ProjectionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectionOrders_Projections_ProjectionId",
                table: "ProjectionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Projections_Movies_MovieId",
                table: "Projections");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId",
                table: "Projections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectionOrders",
                table: "ProjectionOrders");

            migrationBuilder.RenameTable(
                name: "ProjectionOrders",
                newName: "ProjectionOrder");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectionOrders_ProjectionId",
                table: "ProjectionOrder",
                newName: "IX_ProjectionOrder_ProjectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectionOrders_OrderId",
                table: "ProjectionOrder",
                newName: "IX_ProjectionOrder_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectionOrder",
                table: "ProjectionOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectionOrder_Order_OrderId",
                table: "ProjectionOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectionOrder_Projections_ProjectionId",
                table: "ProjectionOrder",
                column: "ProjectionId",
                principalTable: "Projections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
