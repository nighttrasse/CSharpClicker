using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpClicker.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoost_AspNetUsers_UserId",
                table: "UserBoost");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBoost_Boost_BoostId",
                table: "UserBoost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBoost",
                table: "UserBoost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boost",
                table: "Boost");

            migrationBuilder.RenameTable(
                name: "UserBoost",
                newName: "UserBoosts");

            migrationBuilder.RenameTable(
                name: "Boost",
                newName: "Boosts");

            migrationBuilder.RenameIndex(
                name: "IX_UserBoost_BoostId",
                table: "UserBoosts",
                newName: "IX_UserBoosts_BoostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBoosts",
                table: "UserBoosts",
                columns: new[] { "UserId", "BoostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boosts",
                table: "Boosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoosts_AspNetUsers_UserId",
                table: "UserBoosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoosts_Boosts_BoostId",
                table: "UserBoosts",
                column: "BoostId",
                principalTable: "Boosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoosts_AspNetUsers_UserId",
                table: "UserBoosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBoosts_Boosts_BoostId",
                table: "UserBoosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBoosts",
                table: "UserBoosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boosts",
                table: "Boosts");

            migrationBuilder.RenameTable(
                name: "UserBoosts",
                newName: "UserBoost");

            migrationBuilder.RenameTable(
                name: "Boosts",
                newName: "Boost");

            migrationBuilder.RenameIndex(
                name: "IX_UserBoosts_BoostId",
                table: "UserBoost",
                newName: "IX_UserBoost_BoostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBoost",
                table: "UserBoost",
                columns: new[] { "UserId", "BoostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boost",
                table: "Boost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoost_AspNetUsers_UserId",
                table: "UserBoost",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoost_Boost_BoostId",
                table: "UserBoost",
                column: "BoostId",
                principalTable: "Boost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
