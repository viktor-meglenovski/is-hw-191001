using Microsoft.EntityFrameworkCore.Migrations;

namespace repository.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projections_Movies_MovieId",
                table: "Projections");

            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId",
                table: "Projections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId",
                table: "Projections",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_Movies_MovieId",
                table: "Projections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
