using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renamefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_CustomerId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_SellerId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_Users_CustomerId",
                table: "UserRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_Users_SellerId",
                table: "UserRatings");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "UserRatings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "UserRatings",
                newName: "UserWhoRatedId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRatings_SellerId",
                table: "UserRatings",
                newName: "IX_UserRatings_UserId");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ProductImages",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Chats",
                newName: "SecondUserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Chats",
                newName: "FirstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SellerId",
                table: "Chats",
                newName: "IX_Chats_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_CustomerId",
                table: "Chats",
                newName: "IX_Chats_FirstUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_FirstUserId",
                table: "Chats",
                column: "FirstUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_SecondUserId",
                table: "Chats",
                column: "SecondUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_Users_UserId",
                table: "UserRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_Users_UserWhoRatedId",
                table: "UserRatings",
                column: "UserWhoRatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_FirstUserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_SecondUserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_Users_UserId",
                table: "UserRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_Users_UserWhoRatedId",
                table: "UserRatings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRatings",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "UserWhoRatedId",
                table: "UserRatings",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRatings_UserId",
                table: "UserRatings",
                newName: "IX_UserRatings_SellerId");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "ProductImages",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "SecondUserId",
                table: "Chats",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "FirstUserId",
                table: "Chats",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SecondUserId",
                table: "Chats",
                newName: "IX_Chats_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_FirstUserId",
                table: "Chats",
                newName: "IX_Chats_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_CustomerId",
                table: "Chats",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_SellerId",
                table: "Chats",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_Users_CustomerId",
                table: "UserRatings",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_Users_SellerId",
                table: "UserRatings",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
