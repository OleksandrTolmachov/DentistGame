using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistBackend.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_StatsId",
                table: "Users",
                column: "StatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PlayerStats_StatsId",
                table: "Users",
                column: "StatsId",
                principalTable: "PlayerStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PlayerStats_StatsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatsId",
                table: "Users");
        }
    }
}
