using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeimagesstorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "ProductImages");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "UserRatings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "ProductImages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UserRatings");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "ProductImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "ProductImages",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
